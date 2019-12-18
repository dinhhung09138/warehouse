using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Core.Common.Helpers;
using Core.Common.Messages;
using Core.Common.Models;
using Core.Common.Services.Interfaces;
using Warehouse.DataAccess;
using Admin.Service.Constants;
using Admin.Service.Interfaces;
using Z.EntityFramework.Plus;

namespace Admin.Service
{

    /// <summary>
    /// User service.
    /// </summary>
    public class UserService : IUserService
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
        /// <param name="context">data context.</param>
        /// <param name="logger">log service.</param>
        public UserService(IWareHouseUnitOfWork context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get list of user function.
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

                var query = _context.UserRepository.Query()
                                                   .Where(m => m.Deleted == false)
                                                   .Select(m => new Models.UserModel
                                                   {
                                                       Id = m.Id,
                                                       EmployeeId = m.EmployeeId,
                                                       EmployeeName = string.Empty,
                                                       UserName = m.UserName,
                                                       IsActive = m.IsActive,
                                                       LastLogin = m.LastLogin,
                                                   });

                if (filter.Text.Length > 0)
                {
                    query = query.Where(m => m.UserName.ToLower().Contains(filter.Text));
                }

                if (filter.Sort != null)
                {
                    query = query.SortBy(filter.Sort);
                }
                else
                {
                    query = query.OrderBy(m => m.UserName);
                }

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
        /// Create new user account function.
        /// </summary>
        /// <param name="model">User model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Create(Models.UserModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                var checkExistsAccount = await _context.UserRepository.AnyAsync(m => m.EmployeeId == model.EmployeeId)
                                                                      .ConfigureAwait(true);

                if (checkExistsAccount)
                {
                    response.Errors.Add(Message.EmployeeHasAnAccount);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                string password = PasswordSecurityHelper.GetHashedPassword(model.Password);

                await _context.UserRepository.AddAsync(new Warehouse.DataAccess.Entities.User()
                {
                    Id = Guid.NewGuid(),
                    EmployeeId = model.EmployeeId,
                    UserName = model.UserName,
                    Password = password,
                    IsActive = true,
                    CreateBy = model.CurrentUserId,
                    CreateDate = DateTime.Now,
                }).ConfigureAwait(true);

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
        /// Update user account status function.
        /// </summary>
        /// <param name="model">User model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> UpdateActiveStatus(Models.UserModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                var checkExists = await _context.UserRepository
                                                    .AnyAsync(m => m.Id == model.Id)
                                                    .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(Message.AccountNotfound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                string password = PasswordSecurityHelper.GetHashedPassword(model.Password);

                await _context.UserRepository.Query()
                                            .Where(m => m.Id == model.Id)
                                            .UpdateAsync(m => new Warehouse.DataAccess.Entities.User()
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
        /// Delete user account function.
        /// </summary>
        /// <param name="model">User model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Delete(Models.UserModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                var checkExists = await _context.UserRepository
                                                    .AnyAsync(m => m.Id == model.Id)
                                                    .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(Message.AccountNotfound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }
                else
                {
                    await _context.UserRepository.Query()
                                                .Where(m => m.Id == model.Id)
                                                .UpdateAsync(m => new Warehouse.DataAccess.Entities.User()
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
