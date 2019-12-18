using Core.Common.Extensions;
using Core.Common.Helpers;
using Core.Common.Messages;
using Core.Common.Models;
using Core.Common.Services.Interfaces;
using Customer.Service.Constants;
using Customer.Service.Interfaces;
using Customer.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Warehouse.DataAccess;
using Warehouse.DataAccess.Entities;
using Z.EntityFramework.Plus;

namespace Customer.Service
{
    /// <summary>
    /// Customer employee service.
    /// </summary>
    public class CustomerEmployeeService : ICustomerEmployeeService
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
        public CustomerEmployeeService(IWareHouseUnitOfWork context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get list of customer employee data.
        /// </summary>
        /// <param name="filter">Filter model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> List(CustomerFilterModel filter)
        {
            var response = new ResponseModel();
            try
            {
                if (filter == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                var query = from cus in _context.CustomerEmployeeRepository.Query()
                            join store in _context.CustomerStoreRepository.Query()
                            on cus.CustomerStoreId equals store.Id
                            into stores
                            from store in stores.DefaultIfEmpty()
                            where cus.Deleted == false && cus.CreateBy == filter.CustomerId
                            select new CustomerEmployeeModel
                            {
                                Id = cus.Id.ToString(),
                                Code = cus.Code,
                                Name = cus.Name,
                                Phone = cus.Phone,
                                UserName = cus.UserName,
                                Email = cus.Email,
                                StartOn = cus.StartOn,
                                IsActive = cus.IsActive,
                                CustomerStoreName = store.Id != null ? store.Name.ToString() : string.Empty,
                            };

                if (filter.Text.Length > 0)
                {
                    query = query.Where(m => m.Name.ToLower().Contains(filter.Text)
                                            || m.Code.ToLower().Contains(filter.Text)
                                            || m.Phone.ToLower().Contains(filter.Text)
                                            || m.UserName.ToLower().Contains(filter.Text)
                                            || m.Email.ToLower().Contains(filter.Text)
                                            || m.CustomerStoreName.ToLower().Contains(filter.Text));
                }

                query = query.OrderBy(m => m.StartOn);

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
        /// Get list of customer employee data to show on combobox.
        /// </summary>
        /// <param name="customerId">Customer's id</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> ListCombobox(string customerId)
        {
            var response = new ResponseModel();
            try
            {
                var query = _context.CustomerEmployeeRepository.Query()
                                                   .Where(m => m.Deleted == false && m.IsActive == true && m.CreateBy == customerId)
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
        /// Get customer employee detail.
        /// </summary>
        /// <param name="id">Customer employee's id.</param>
        /// <param name="customerId">Customer's id</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Detail(Guid id, string customerId)
        {
            var response = new ResponseModel();
            try
            {
                var item = await _context.CustomerEmployeeRepository.FirstOrDefaultAsync(m => m.Deleted == false
                                                                                                && m.Id == id
                                                                                                && m.CreateBy == customerId)
                                                            .ConfigureAwait(false);

                if (item == null)
                {
                    throw new Exception(CommonMessage.IdNotFound);
                }

                CustomerEmployeeModel md = new CustomerEmployeeModel();
                md.Id = item.Id.ToString();
                md.Code = item.Code;
                md.Name = item.Name;
                md.AvatarFileId = item.AvatarFileId.HasValue ? string.Empty : item.AvatarFileId.ToString();
                md.Phone = item.Phone;
                md.Email = item.Email;
                md.UserName = item.UserName;
                md.StartOn = item.StartOn;
                md.Email = item.Email;
                md.CustomerStoreId = !item.CustomerStoreId.HasValue ? string.Empty : item.CustomerStoreId.ToString();
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
        /// Save a customer employee function.
        /// </summary>
        /// <param name="model">Customer employee model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Save(CustomerEmployeeModel model)
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

                    var checkExists = await _context.CustomerEmployeeRepository
                                                        .AnyAsync(m => m.Id == id 
                                                                       && m.CreateBy == model.CurrentUserId)
                                                        .ConfigureAwait(false);

                    if (!checkExists)
                    {
                        response.Errors.Add(CommonMessage.IdNotFound);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    var checkCurrent = await _context.CustomerEmployeeRepository
                                                        .AnyAsync(m => m.Id == id
                                                                       && m.CreateBy == model.CurrentUserId
                                                                       && m.RowVersion != model.RowVersion)
                                                        .ConfigureAwait(false);

                    if (checkCurrent)
                    {
                        response.Errors.Add(CommonMessage.DataUpdatedByOtherUser);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }
                    
                    checkExists = await _context.CustomerEmployeeRepository
                                                        .AnyAsync(m => m.Id != id
                                                                       && m.CreateBy == model.CurrentUserId
                                                                       && m.Code == model.Code)
                                                        .ConfigureAwait(false);

                    if (checkExists)
                    {
                        response.Errors.Add(Message.CodeIsExists);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    await _context.CustomerEmployeeRepository.Query()
                        .Where(m => m.Id == id)
                        .UpdateAsync(m => new CustomerEmployee()
                        {
                            Code = model.Code,
                            Name = model.Name,
                            AvatarFileId = null, // TOOD
                            Phone = model.Phone,
                            Email = model.Email,
                            StartOn = model.StartOn.HasValue ? model.StartOn.Value.ToLocalTime() : model.StartOn,
                            CustomerStoreId = (model.CustomerStoreId.Length > 0 ? new Guid(model.CustomerStoreId) : default(Guid)),
                            IsActive = model.IsActive,
                            UpdateBy = model.CurrentUserId,
                            UpdateDate = DateTime.Now,
                        }).ConfigureAwait(false);
                }
                else
                {
                    var checkExists = await _context.CustomerEmployeeRepository
                                                        .AnyAsync(m => m.CreateBy == model.CurrentUserId
                                                                       && m.Code == model.Code)
                                                        .ConfigureAwait(false);

                    if (checkExists)
                    {
                        response.Errors.Add(Message.CodeIsExists);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    string password = string.Empty;

                    if (model.UserName.Length > 0)
                    {
                        password = PasswordSecurityHelper.GetHashedPassword(model.Password);
                    }

                    await _context.CustomerEmployeeRepository.AddAsync(new CustomerEmployee()
                    {
                        Id = Guid.NewGuid(),
                        Code = model.Code,
                        Name = model.Name,
                        AvatarFileId = null, // TOOD
                        Phone = model.Phone,
                        Email = model.Email,
                        StartOn = model.StartOn.HasValue ? model.StartOn.Value.ToLocalTime() : model.StartOn,
                        CustomerStoreId = (model.CustomerStoreId.Length > 0 ? new Guid(model.CustomerStoreId) : default(Guid)),
                        UserName = model.UserName,
                        Password = password,
                        IsActive = model.IsActive,
                        CreateBy = model.CurrentUserId,
                        CreateDate = DateTime.Now,
                        Deleted = false,
                    }).ConfigureAwait(false);
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
        /// Update customer employee status function.
        /// </summary>
        /// <param name="model">Customer employee model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> UpdateActiveStatus(CustomerEmployeeModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                Guid id = new Guid(model.Id);

                var checkExists = await _context.CustomerEmployeeRepository
                                                            .AnyAsync(m => m.Id == id)
                                                            .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                await _context.CustomerEmployeeRepository.Query().Where(m => m.Id == id)
                                                         .UpdateAsync(m => new CustomerEmployee()
                                                         {
                                                             IsActive = model.IsActive,
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
        /// Create User name for employee.
        /// </summary>
        /// <param name="model">Customer employee model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> CreateUserAccount(CustomerEmployeeModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                Guid id = new Guid(model.Id);
                string password = PasswordSecurityHelper.GetHashedPassword(model.Password);

                var checkExists = await _context.CustomerEmployeeRepository
                                                            .AnyAsync(m => m.Id == id 
                                                                           && m.CreateBy == model.CurrentUserId)
                                                            .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                checkExists = await _context.CustomerEmployeeRepository
                                                            .AnyAsync(m => m.UserName == model.UserName 
                                                                           && m.CreateBy == model.CurrentUserId
                                                                           && m.Deleted == false)
                                                            .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(Message.UserNameIsExists);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }
                
                await _context.CustomerEmployeeRepository.Query().Where(m => m.Id == id)
                                                        .UpdateAsync(m => new CustomerEmployee()
                                                        {
                                                            UserName = model.UserName,
                                                            Password = password,
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
        /// Update passowrd for employee.
        /// </summary>
        /// <param name="model">Customer employee model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> UpdatePassword(CustomerEmployeeModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                Guid id = new Guid(model.Id);
                string password = PasswordSecurityHelper.GetHashedPassword(model.Password);

                var checkExists = await _context.CustomerEmployeeRepository
                                                            .AnyAsync(m => m.Id == id
                                                                           && m.CreateBy == model.CurrentUserId)
                                                            .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }
                
                await _context.CustomerEmployeeRepository.Query().Where(m => m.Id == id)
                                                        .UpdateAsync(m => new CustomerEmployee()
                                                        {
                                                            Password = password,
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
        /// Delete customer employee function.
        /// </summary>
        /// <param name="model">Customer employee model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Delete(CustomerEmployeeModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                Guid id = new Guid(model.Id);

                var checkExists = await _context.CustomerEmployeeRepository
                                                        .AnyAsync(m => m.Id == id 
                                                                       && m.CreateBy == model.CurrentUserId)
                                                        .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                await _context.CustomerEmployeeRepository.Query()
                                                       .Where(m => m.Id == id)
                                                       .UpdateAsync(m => new CustomerEmployee()
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
