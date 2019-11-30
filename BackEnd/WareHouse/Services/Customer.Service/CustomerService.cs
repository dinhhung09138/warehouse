using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Core.Common.Messages;
using Core.Common.Models;
using Core.Common.Services.Interfaces;
using Customer.Service.Interfaces;
using Customer.Service.Models;
using Warehouse.DataAccess;
using Warehouse.DataAccess.Entities;
using Z.EntityFramework.Plus;

namespace Customer.Service.Partner
{
    /// <summary>
    /// Customer service.
    /// </summary>
    public class CustomerService : ICustomerService
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
        public CustomerService(IWareHouseUnitOfWork context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get list of customer data.
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

                var query = _context.CustomerRepository.Query()
                                                   .Where(m => m.Deleted == false)
                                                   .Select(m => new CustomerModel
                                                   {
                                                       Id = m.Id,
                                                       LogoFileId = m.LogoFileId,
                                                       Name = m.Name,
                                                       PrimaryPhone = m.PrimaryPhone,
                                                       Website = m.Website,
                                                       IsCompany = m.IsCompany,
                                                       StartOn = m.StartOn,
                                                       IsActive = m.IsActive,
                                                       RowVersion = m.RowVersion,
                                                   });

                if (filter.Text.Length > 0)
                {
                    query = query.Where(m => m.Name.ToLower().Contains(filter.Text)
                                            || m.PrimaryPhone.ToLower().Contains(filter.Text)
                                            || m.Website.ToLower().Contains(filter.Text)
                                            || m.Name.ToLower().Contains(filter.Text)
                                            || m.Name.ToLower().Contains(filter.Text));
                }

                if (filter.Sort != null)
                {
                    query = query.SortBy(filter.Sort);
                }
                else
                {
                    query = query.OrderBy(m => m.Name);
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
        /// Get customer detail.
        /// </summary>
        /// <param name="id">Customer's id.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Detail(Guid id)
        {
            var response = new ResponseModel();
            try
            {
                var item = await _context.CustomerRepository.FirstOrDefaultAsync(m => m.Deleted == false
                                                                                      && m.Id == id)
                                                            .ConfigureAwait(false);

                if (item == null)
                {
                    throw new Exception(CommonMessage.IdNotFound);
                }

                CustomerModel md = new CustomerModel();
                md.Id = item.Id;
                md.Name = item.Name;
                md.LogoFileId = item.LogoFileId;
                md.PrimaryPhone = item.PrimaryPhone;
                md.SecondaryPhone = item.SecondaryPhone;
                md.Fax = item.Fax;
                md.Website = item.Website;
                md.TaxCode = item.TaxCode;
                md.IsCompany = item.IsCompany;
                md.StartOn = item.StartOn;
                md.Description = item.Description;
                md.Address = item.Address;
                md.CitiId = item.CitiId;
                md.CountryId = item.CountryId;
                md.Longtitue = item.Longtitue;
                md.Latitude = item.Latitude;
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
        /// Save a customer function.
        /// </summary>
        /// <param name="model">Customer model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Save(CustomerModel model)
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
                    var checkExists = await _context.CustomerRepository
                                                        .AnyAsync(m => m.Id == model.Id)
                                                        .ConfigureAwait(false);

                    if (!checkExists)
                    {
                        response.Errors.Add(CommonMessage.IdNotFound);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    var checkCurrent = await _context.CustomerRepository
                                                        .AnyAsync(m => m.Id == model.Id
                                                                       && m.RowVersion == model.RowVersion)
                                                        .ConfigureAwait(false);

                    if (!checkCurrent)
                    {
                        response.Errors.Add(CommonMessage.DataUpdatedByOtherUser);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    await _context.CustomerRepository.Query()
                                                     .Where(m => m.Id == model.Id)
                                                     .UpdateAsync(m => new Warehouse.DataAccess.Entities.Customer()
                                                     {
                                                         Name = model.Name,
                                                         LogoFileId = model.LogoFileId,
                                                         PrimaryPhone = model.PrimaryPhone,
                                                         SecondaryPhone = model.SecondaryPhone,
                                                         Fax = model.Fax,
                                                         Website = model.Website,
                                                         TaxCode = model.TaxCode,
                                                         IsCompany = model.IsCompany,
                                                         StartOn = model.StartOn,
                                                         Description = model.Description,
                                                         Address = model.Address,
                                                         CitiId = model.CitiId,
                                                         CountryId = model.CountryId,
                                                         Longtitue = model.Longtitue,
                                                         Latitude = model.Latitude,
                                                         IsActive = model.IsActive,
                                                         UpdateBy = model.CurrentUserId,
                                                         UpdateDate = DateTime.Now,
                                                     }).ConfigureAwait(true);
                }
                else
                {
                    await _context.CustomerRepository.AddAsync(new Warehouse.DataAccess.Entities.Customer()
                    {
                        Id = Guid.NewGuid(),
                        LogoFileId = model.LogoFileId,
                        PrimaryPhone = model.PrimaryPhone,
                        SecondaryPhone = model.SecondaryPhone,
                        Fax = model.Fax,
                        Website = model.Website,
                        TaxCode = model.TaxCode,
                        IsCompany = model.IsCompany,
                        StartOn = model.StartOn,
                        Description = model.Description,
                        Address = model.Address,
                        CitiId = model.CitiId,
                        CountryId = model.CountryId,
                        Longtitue = model.Longtitue,
                        Latitude = model.Latitude,
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
        /// Update customer status function.
        /// </summary>
        /// <param name="model">Customer model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> UpdateActiveStatus(CustomerModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                var checkExistsAccount = await _context.CustomerRepository
                                                            .AnyAsync(m => m.Id == model.Id)
                                                            .ConfigureAwait(true);

                if (!checkExistsAccount)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }
                else
                {
                    await _context.CustomerRepository.Query().Where(m => m.Id == model.Id)
                                                         .UpdateAsync(m => new Warehouse.DataAccess.Entities.Customer()
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
        /// Delete customer function.
        /// </summary>
        /// <param name="model">Customer model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Delete(CustomerModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                var checkExistsAccount = await _context.UserRepository
                                                        .AnyAsync(m => m.Id == model.Id)
                                                        .ConfigureAwait(true);

                if (!checkExistsAccount)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }
                else
                {
                    await _context.CustomerRepository.Query()
                                                        .Where(m => m.Id == model.Id)
                                                        .UpdateAsync(m => new Warehouse.DataAccess.Entities.Customer()
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
