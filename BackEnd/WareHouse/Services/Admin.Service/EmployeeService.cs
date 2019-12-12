using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Admin.Service.Constants;
using Admin.Service.Interfaces;
using Admin.Service.Models;
using Core.Common.Extensions;
using Core.Common.Messages;
using Core.Common.Models;
using Core.Common.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Warehouse.DataAccess;
using Warehouse.DataAccess.Entities;
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
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="context">Data context.</param>
        /// <param name="logger">Log service.</param>
        public EmployeeService(IWareHouseUnitOfWork context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get list of employee unit data.
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

                var query = _context.EmployeeRepository.Query()
                                                   .Where(m => m.Deleted == false)
                                                   .Select(m => new EmployeeModel
                                                   {
                                                       Id = m.Id.ToString(),
                                                       Code = m.Code,
                                                       Name = m.Name,
                                                       Mobile = m.Mobile,
                                                       Email = m.Email,
                                                       IsActive = m.IsActive,
                                                       RowVersion = m.RowVersion,
                                                   });

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
                                                       Value = m.Id.ToString(),
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
                md.AvatarFileId = item.AvatarFileId.HasValue ? string.Empty : item.AvatarFileId.ToString();
                md.Mobile = item.Mobile;
                md.WorkPhone = item.WorkPhone;
                md.Fax = item.Fax;
                md.DateOfJoin = item.DateOfJoin;
                md.DateOfLeaving = item.DateOfLeaving;
                md.Email = item.Email;
                md.DepartmentId = item.DepartmentId.HasValue ? string.Empty : item.DepartmentId.ToString();
                md.IsActive = item.IsActive;
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

                if (model.IsEdit)
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

                    await _context.EmployeeRepository.Query()
                        .Where(m => m.Id == id)
                        .UpdateAsync(m => new Employee()
                        {
                            Code = model.Code,
                            Name = model.Name,
                            AvatarFileId = null, // TOOD
                            Mobile = model.Mobile,
                            WorkPhone = model.WorkPhone,
                            Fax = model.Fax,
                            DateOfJoin = model.DateOfJoin,
                            DateOfLeaving = model.DateOfLeaving,
                            Email = model.Email,
                            DepartmentId = null, //TODO
                            IsActive = model.IsActive,
                            UpdateBy = model.CurrentUserId,
                            UpdateDate = DateTime.Now,
                        }).ConfigureAwait(true);
                }
                else
                {
                    var checkCode = await _context.EmployeeRepository.AnyAsync(m => m.Code == model.Code);
                    if (checkCode)
                    {
                        response.Errors.Add(Message.CodeIsExists);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    await _context.EmployeeRepository.AddAsync(new Employee()
                    {
                        Id = Guid.NewGuid(),
                        Code = model.Code,
                        Name = model.Name,
                        AvatarFileId = null, // TOOD
                        Mobile = model.Mobile,
                        WorkPhone = model.WorkPhone,
                        Fax = model.Fax,
                        DateOfJoin = model.DateOfJoin,
                        DateOfLeaving = model.DateOfLeaving,
                        Email = model.Email,
                        DepartmentId = null, //TODO
                        IsActive = model.IsActive,
                        CreateBy = model.CurrentUserId,
                        CreateDate = DateTime.Now,
                        Deleted = false,
                    }).ConfigureAwait(true);
                }

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

                var checkExistsAccount = await _context.EmployeeRepository
                                                            .AnyAsync(m => m.Id == id)
                                                            .ConfigureAwait(true);

                if (!checkExistsAccount)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }
                else
                {
                    await _context.EmployeeRepository.Query().Where(m => m.Id == id)
                                                         .UpdateAsync(m => new Employee()
                                                         {
                                                             IsActive = model.IsActive,
                                                             UpdateBy = model.CurrentUserId,
                                                             UpdateDate = DateTime.Now,
                                                         }).ConfigureAwait(false);

                    await _context.SaveChangesAsync().ConfigureAwait(false);
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
                else
                {
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
