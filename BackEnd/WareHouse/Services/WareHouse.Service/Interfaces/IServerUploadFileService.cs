using Core.Common.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace WareHouse.Service.Interfaces
{
    /// <summary>
    /// Server upload file service interface.
    /// </summary>
    public interface IServerUploadFileService
    {
        /// <summary>
        /// Upload file to folder in the server.
        /// Read server config path with key: [UploadFolderPath]
        /// </summary>
        /// <param name="file">File upload</param>
        /// <param name="fileId">File's Id</param>
        /// <param name="currentUserId">Current user id</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UploadFile(IFormFile file, string fileId, string currentUserId);

        /// <summary>
        /// Delete file in server.
        /// Read server config path with key: [UploadFolderPath]
        /// </summary>
        /// <param name="fileId">File's id</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> DeleteFile(string fileId);

        /// <summary>
        /// Get Image content
        /// </summary>
        /// <param name="fileId">Image file's id</param>
        /// <returns></returns>
        Task<string> ImageContent(string fileId);
    }
}
