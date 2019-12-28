using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Admin.Service.Constants;
using Admin.Service.Interfaces;
using Admin.Service.Models;
using Core.Common.Constants;
using Core.Common.Extensions;
using Core.Common.Messages;
using Core.Common.Models;
using Core.Common.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Warehouse.DataAccess;
using Warehouse.DataAccess.Entities;
using WareHouse.Service.Interfaces;
using Z.EntityFramework.Plus;

namespace Admin.Service
{
    /// <summary>
    /// Employee service.
    /// </summary>
    public class EmployeeService : IEmployeeService
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
        /// File service interface.
        /// </summary>
        private readonly IFileService _fileService;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="context">Data context.</param>
        /// <param name="logger">Log service.</param>
        public EmployeeService(IWareHouseUnitOfWork context, ILoggerService logger, IFileService fileService)
        {
            _context = context;
            _logger = logger;
            _fileService = fileService;
        }

        /// <summary>
        /// Get list of employee data.
        /// </summary>
        /// <param name="filter">Filter model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> List(FilterModel filter)
        {
            var response = new ResponseModel();
            try
            {
                if (filter == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                var query = from empl in _context.EmployeeRepository.Query()
                            join dept in _context.DepartmentRepository.Query()
                            on empl.DepartmentId equals dept.Id
                            into departments
                            from dept in departments.DefaultIfEmpty()
                            where empl.Deleted == false
                            select new EmployeeModel
                            {
                                Id = empl.Id.ToString(),
                                Code = empl.Code,
                                Name = empl.Name,
                                Mobile = empl.Mobile,
                                Email = empl.Email,
                                DepartmentName = dept.Name,
                                IsActive = empl.IsActive ? "1" : "0",
                                RowVersion = empl.RowVersion,
                            };

                if (filter.Text.Length > 0)
                {
                    query = query.Where(m => m.Code.ToLower().Contains(filter.Text)
                                            || m.Name.ToLower().Contains(filter.Text));
                }

                query = query.OrderBy(m => m.Code);

                response.Result = await query.ToBaseList(filter.Paging?.PageIndex, filter.Paging?.PageSize).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.AddErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, filter, ex);
                response.ResponseStatus = Core.Common.Enums.ResponseStatus.Error;
            }

            return response;
        }

        /// <summary>
        /// Get list of employee data to show on combobox.
        /// </summary>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> ListCombobox()
        {
            var response = new ResponseModel();
            try
            {
                var query = _context.EmployeeRepository.Query()
                                                   .Where(m => m.Deleted == false && m.IsActive == true)
                                                   .Select(m => new SelectedItemModel
                                                   {
                                                       Value = m.Id.ToString().ToLower(),
                                                       Title = m.Name,
                                                   });

                response.Result = await query.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.AddErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, ex);
                response.ResponseStatus = Core.Common.Enums.ResponseStatus.Error;
            }

            return response;
        }

        /// <summary>
        /// Get employee detail.
        /// </summary>
        /// <param name="id">Employee's id.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Detail(Guid id)
        {
            var response = new ResponseModel();
            try
            {
                var item = await _context.EmployeeRepository.FirstOrDefaultAsync(m => m.Deleted == false
                                                                                      && m.Id == id)
                                                            .ConfigureAwait(false);

                if (item == null)
                {
                    throw new Exception(CommonMessage.IdNotFound);
                }

                EmployeeModel md = new EmployeeModel();
                md.Id = item.Id.ToString();
                md.Name = item.Name;
                md.Code = item.Code;
                if (item.AvatarFileId.HasValue)
                {
                    md.AvatarFileId = item.AvatarFileId.ToString();
                    md.AvatarContent = await _fileService.ImageContent(item.AvatarFileId.ToString()).ConfigureAwait(false);
                }
                md.Mobile = item.Mobile;
                md.WorkPhone = item.WorkPhone;
                md.Fax = item.Fax;
                md.DateOfJoin = item.DateOfJoin;
                md.DateOfLeaving = item.DateOfLeaving;
                md.Email = item.Email;
                md.DepartmentId = !item.DepartmentId.HasValue ? string.Empty : item.DepartmentId.ToString();
                md.IsActive = item.IsActive ? "1" : "0";
                md.RowVersion = item.RowVersion;

                response.Result = md;
            }
            catch (Exception ex)
            {
                _logger.AddErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, id, ex);
                response.ResponseStatus = Core.Common.Enums.ResponseStatus.Error;
            }

            return response;
        }

        /// <summary>
        /// Save a employee function.
        /// </summary>
        /// <param name="model">Employee model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Save(EmployeeModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                //DateOfJoinString alway have value
                model.DateOfJoin = model.DateOfJoinString.ToDate().Value;
                model.DateOfLeaving = model.DateOfLeavingString.ToDate();

                string avatarId = string.Empty;

                if (model.File != null)
                {
                    avatarId = Guid.NewGuid().ToString();
                    await _fileService.UploadFile(model.File, avatarId, model.CurrentUserId).ConfigureAwait(false);
                }

                if (model.IsEdit == FormStatus.Update)
                {
                    Guid id = new Guid(model.Id);

                    var checkExists = await _context.EmployeeRepository
                                                        .AnyAsync(m => m.Id == id)
                                                        .ConfigureAwait(false);

                    if (!checkExists)
                    {
                        response.Errors.Add(CommonMessage.IdNotFound);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    checkExists = await _context.EmployeeRepository
                                                        .AnyAsync(m => m.Id != id
                                                                       && m.Code == model.Code)
                                                        .ConfigureAwait(false);

                    if (checkExists)
                    {
                        response.Errors.Add(Message.CodeIsExists);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    var checkCurrent = await _context.EmployeeRepository
                                                        .AnyAsync(m => m.Id == id
                                                                       && m.RowVersion != model.RowVersion)
                                                        .ConfigureAwait(false);

                    if (checkCurrent)
                    {
                        response.Errors.Add(CommonMessage.DataUpdatedByOtherUser);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    if (!string.IsNullOrEmpty(model.AvatarFileId) && !string.IsNullOrEmpty(avatarId))
                    {
                        await _fileService.DeleteFile(model.AvatarFileId).ConfigureAwait(false);
                    }

                    var md = await _context.EmployeeRepository.FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(false);

                    md.Code = model.Code;
                    md.Name = model.Name;

                    if (!string.IsNullOrEmpty(avatarId))
                    {
                        md.AvatarFileId = new Guid(avatarId);
                    }

                    md.Mobile = model.Mobile;
                    md.WorkPhone = model.WorkPhone;
                    md.Fax = model.Fax;
                    md.DateOfJoin = model.DateOfJoin;
                    md.DateOfLeaving = model.DateOfLeaving.HasValue ? model.DateOfLeaving.Value : model.DateOfLeaving;
                    md.Email = model.Email;
                    md.DepartmentId = (model.DepartmentId.Length > 0 ? new Guid(model.DepartmentId) : default(Guid));
                    md.IsActive = model.IsActive == "1" ? true : false;
                    md.UpdateBy = model.CurrentUserId;
                    md.UpdateDate = DateTime.Now;

                    _context.EmployeeRepository.Update(md);
                    await _context.SaveChangesAsync().ConfigureAwait(false);
                }
                else
                {
                    var checkCode = await _context.EmployeeRepository.AnyAsync(m => m.Code == model.Code).ConfigureAwait(true);
                    if (checkCode)
                    {
                        response.Errors.Add(Message.CodeIsExists);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    Employee md = new Employee();

                    md.Id = Guid.NewGuid();
                    md.Code = model.Code;
                    md.Name = model.Name;

                    if (!string.IsNullOrEmpty(avatarId))
                    {
                        md.AvatarFileId = new Guid(avatarId);
                    }

                    md.Mobile = model.Mobile;
                    md.WorkPhone = model.WorkPhone;
                    md.Fax = model.Fax;
                    md.DateOfJoin = model.DateOfJoin;
                    md.DateOfLeaving = model.DateOfLeaving.HasValue ? model.DateOfLeaving.Value : model.DateOfLeaving;
                    md.Email = model.Email;
                    md.DepartmentId = (model.DepartmentId.Length > 0 ? new Guid(model.DepartmentId) : default(Guid));
                    md.IsActive = model.IsActive == "1" ? true : false;
                    md.CreateBy = model.CurrentUserId;
                    md.CreateDate = DateTime.Now;
                    md.Deleted = false;

                    await _context.EmployeeRepository.AddAsync(md).ConfigureAwait(true);
                    await _context.SaveChangesAsync();
                }

            }
            catch (Exception ex)
            {
                _logger.AddErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, model, ex);
                response.ResponseStatus = Core.Common.Enums.ResponseStatus.Error;
            }

            return response;
        }

        /// <summary>
        /// Update employee status function.
        /// </summary>
        /// <param name="model">Employee model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> UpdateActiveStatus(EmployeeModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                Guid id = new Guid(model.Id);

                var checkExists = await _context.EmployeeRepository
                                                        .AnyAsync(m => m.Id == id)
                                                        .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                await _context.EmployeeRepository.Query()
                                                .Where(m => m.Id == id)
                                                .UpdateAsync(m => new Employee()
                                                {
                                                    IsActive = model.IsActive == "1" ? true : false,
                                                    UpdateBy = model.CurrentUserId,
                                                    UpdateDate = DateTime.Now,
                                                }).ConfigureAwait(false);

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.AddErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, model, ex);
                response.ResponseStatus = Core.Common.Enums.ResponseStatus.Error;
            }

            return response;
        }

        /// <summary>
        /// Delete employee function.
        /// </summary>
        /// <param name="model">Employee model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Delete(EmployeeModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                Guid id = new Guid(model.Id);

                var checkExists = await _context.EmployeeRepository
                                                        .AnyAsync(m => m.Id == id)
                                                        .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                var md = await _context.EmployeeRepository.FirstOrDefaultAsync(m => m.Id == id).ConfigureAwait(true);

                if (md.AvatarFileId.HasValue)
                {
                    await _fileService.DeleteFile(md.AvatarFileId.ToString());
                }

                await _context.EmployeeRepository.Query()
                                                .Where(m => m.Id == id)
                                                .UpdateAsync(m => new Employee()
                                                {
                                                    Deleted = true,
                                                    UpdateBy = model.CurrentUserId,
                                                    UpdateDate = DateTime.Now,
                                                    DeleteBy = model.CurrentUserId,
                                                    DeleteDate = DateTime.Now,
                                                }).ConfigureAwait(false);

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                _logger.AddErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, model, ex);
                response.ResponseStatus = Core.Common.Enums.ResponseStatus.Error;
            }

            return response;
        }
    }
}
