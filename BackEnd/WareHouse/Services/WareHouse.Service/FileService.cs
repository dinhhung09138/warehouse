using Core.Common.Messages;
using Core.Common.Models;
using Core.Common.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Warehouse.DataAccess;
using WareHouse.Service.Interfaces;

namespace WareHouse.Service
{
    /// <summary>
    /// Server upload file service.
    /// </summary>
    public class FileService : IFileService
    {
        /// <summary>
        /// Data context.
        /// </summary>
        private readonly IWareHouseUnitOfWork _context;

        /// <summary>
        /// Log service.
        /// </summary>
        private readonly ILoggerService _logger;

        /// <summary>
        /// Configuration.
        /// </summary>
        private readonly IConfiguration _config;

        /// <summary>
        /// Server file upload service.
        /// </summary>
        private readonly IServerUploadFileService _file;

        /// <summary>
        /// Server config key.
        /// </summary>
        private readonly string serverPathKeyConfig = "UploadFolderPath";

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="context">Data context.</param>
        /// <param name="logger">Log service.</param>
        public FileService(
            IWareHouseUnitOfWork context,
            ILoggerService logger,
            IConfiguration config,
            IServerUploadFileService file)
        {
            _context = context;
            _logger = logger;
            _config = config;
            _file = file;
        }

        /// <summary>
        /// Upload file to folder in the server.
        /// Read server config path with key: [UploadFolderPath]
        /// </summary>
        /// <param name="file">File upload</param>
        /// <param name="fileId">File's Id</param>
        /// <param name="currentUserId">Current user id</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> UploadFile(IFormFile file, string fileId, string currentUserId)
        {
            var response = new ResponseModel();
            try
            {
                string fileName = file.FileName;
                string fileExt = Path.GetExtension(file.FileName).Replace(".", "");
                string newFileName = $"{DateTime.Now.ToString("ddMMyyyyhhmmss")}.{fileExt}";

                string folderPath = _config[serverPathKeyConfig];
                string filePath = Path.Combine(folderPath, newFileName);

                response = await _file.UploadFile(file, folderPath, newFileName).ConfigureAwait(true);
                if (response.ResponseStatus != Core.Common.Enums.ResponseStatus.Success)
                {
                    return response;
                }

                Warehouse.DataAccess.Entities.File uploadFile = new Warehouse.DataAccess.Entities.File();
                uploadFile.Id = new Guid(fileId);
                uploadFile.FileId = string.Empty;
                uploadFile.FileName = fileName;
                uploadFile.FileExt = fileExt;
                uploadFile.FileSystemName = newFileName;
                uploadFile.FilePath = filePath;
                uploadFile.Size = file.Length;
                uploadFile.CreateDate = DateTime.Now;
                uploadFile.CreateBy = currentUserId;

                await _context.FileRepository.AddAsync(uploadFile).ConfigureAwait(true);
                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.AddErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, currentUserId, ex);
                response.ResponseStatus = Core.Common.Enums.ResponseStatus.Error;
            }

            return response;
        }

        /// <summary>
        /// Delete file in server.
        /// Read server config path with key: [UploadFolderPath]
        /// </summary>
        /// <param name="fileId">File's id</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> DeleteFile(string fileId)
        {
            var response = new ResponseModel();
            try
            {
                Warehouse.DataAccess.Entities.File uploadFile = await _context.FileRepository.FirstOrDefaultAsync(m => m.Id == new Guid(fileId));

                if (uploadFile == null)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                response = await _file.DeleteFile(uploadFile.FilePath).ConfigureAwait(true);
                if (response.ResponseStatus != Core.Common.Enums.ResponseStatus.Success)
                {
                    return response;
                }

                _context.FileRepository.Delete(uploadFile);

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.AddErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, fileId, ex);
                response.ResponseStatus = Core.Common.Enums.ResponseStatus.Error;
            }

            return response;
        }

        /// <summary>
        /// Get Image content
        /// </summary>
        /// <param name="fileId">Image file's id</param>
        /// <returns></returns>
        public async Task<string> ImageContent(string fileId)
        {
            try
            {
                Warehouse.DataAccess.Entities.File uploadFile = await _context.FileRepository.FirstOrDefaultAsync(m => m.Id == new Guid(fileId));

                if (uploadFile == null)
                {
                    return string.Empty;
                }

                return await _file.ImageContent(uploadFile.FilePath, uploadFile.FileExt);
            }
            catch (Exception ex)
            {
                _logger.AddErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, fileId, ex);
                return string.Empty;
            }
        }
    }
}
