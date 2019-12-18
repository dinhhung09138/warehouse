using Core.Common.Extensions;
using Core.Common.Messages;
using Core.Common.Models;
using Core.Common.Services.Interfaces;
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
    /// Customer store service.
    /// </summary>
    public class CustomerStoreService : ICustomerStoreService
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
        public CustomerStoreService(IWareHouseUnitOfWork context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get list of customer store data.
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

                var query = from cus in _context.CustomerStoreRepository.Query()
                            join country in _context.CountryRepository.Query() on cus.CountryId equals country.Id
                            into countries
                            from country in countries.DefaultIfEmpty()
                            join city in _context.CityRepository.Query() on cus.CityId equals city.Id
                            into cities
                            from city in cities.DefaultIfEmpty()
                            join empl in _context.CustomerEmployeeRepository.Query() on cus.StoreManagerId equals empl.Id
                            into empls
                            from empl in empls.DefaultIfEmpty()
                            where cus.Deleted == false && cus.CreateBy == filter.CustomerId
                            select new CustomerStoreModel
                            {
                                Id = cus.Id.ToString(),
                                Name = cus.Name,
                                PrimaryPhone = cus.PrimaryPhone,
                                Fax = cus.Fax,
                                StartOn = cus.StartOn,
                                Address = cus.Address,
                                IsActive = cus.IsActive,
                                CountryId = country.Id != null ? country.Id.ToString() : string.Empty,
                                CountryName = country.Id != null ? country.Name.ToString() : string.Empty,
                                StoreManagerName = empl.Id != null ? empl.Name.ToString() : string.Empty,
                                CityId = city.Id != null ? city.Id.ToString() : string.Empty,
                                CityName = city.Id != null ? city.Name.ToString() : string.Empty,
                            };

                if (filter.Text.Length > 0)
                {
                    query = query.Where(m => m.Name.ToLower().Contains(filter.Text)
                                            || m.PrimaryPhone.ToLower().Contains(filter.Text)
                                            || m.Fax.ToLower().Contains(filter.Text)
                                            || m.CityName.ToLower().Contains(filter.Text)
                                            || m.CountryName.ToLower().Contains(filter.Text)
                                            || m.StoreManagerName.ToLower().Contains(filter.Text)
                                            || m.Address.ToLower().Contains(filter.Text));
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
        /// Get list of customer's store data to show on combobox.
        /// </summary>
        /// <param name="customerId">Customer's id</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> ListCombobox(string customerId)
        {
            var response = new ResponseModel();
            try
            {
                var query = _context.CustomerStoreRepository.Query()
                                                   .Where(m => m.Deleted == false 
                                                               && m.IsActive == true
                                                               && m.CreateBy == customerId)
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
        /// Get customer store detail.
        /// </summary>
        /// <param name="id">Customer store's id.</param>
        /// <param name="customerId">Customer's id</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Detail(Guid id, string customerId)
        {
            var response = new ResponseModel();
            try
            {
                var item = await _context.CustomerStoreRepository.FirstOrDefaultAsync(m => m.Deleted == false
                                                                                      && m.Id == id
                                                                                      && m.CreateBy == customerId)
                                                            .ConfigureAwait(false);

                if (item == null)
                {
                    throw new Exception(CommonMessage.IdNotFound);
                }

                CustomerStoreModel md = new CustomerStoreModel();
                md.Id = item.Id.ToString();
                md.Name = item.Name;
                md.PrimaryPhone = item.PrimaryPhone;
                md.SecondaryPhone = item.SecondaryPhone;
                md.Fax = item.Fax;
                md.StoreManagerId = !item.StoreManagerId.HasValue ? string.Empty : item.StoreManagerId.ToString();
                md.StartOn = item.StartOn;
                md.Description = item.Description;
                md.Address = item.Address;
                md.CityId = !item.CityId.HasValue ? string.Empty : item.CityId.ToString();
                md.CountryId = !item.CountryId.HasValue ? string.Empty : item.CountryId.ToString();
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
        /// Save a customer store function.
        /// </summary>
        /// <param name="model">Customer store model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Save(CustomerStoreModel model)
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

                    var checkExists = await _context.CustomerStoreRepository
                                                        .AnyAsync(m => m.Id == id
                                                                       && m.CreateBy == model.CurrentUserId)
                                                        .ConfigureAwait(false);

                    if (!checkExists)
                    {
                        response.Errors.Add(CommonMessage.IdNotFound);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    var checkCurrent = await _context.CustomerStoreRepository
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

                    await _context.CustomerStoreRepository.Query()
                        .Where(m => m.Id == id)
                        .UpdateAsync(m => new CustomerStore()
                        {
                            Name = model.Name,
                            PrimaryPhone = model.PrimaryPhone,
                            SecondaryPhone = model.SecondaryPhone,
                            Fax = model.Fax,
                            StoreManagerId = (model.StoreManagerId.Length > 0 ? new Guid(model.StoreManagerId) : default(Guid)),
                            StartOn = model.StartOn.HasValue ? model.StartOn.Value.ToLocalTime() : model.StartOn,
                            Description = model.Description,
                            Address = model.Address,
                            Longtitue = model.Longtitue,
                            Latitude = model.Latitude,
                            CityId = (model.CityId.Length > 0 ? new Guid(model.CityId) : default(Guid)),
                            CountryId = (model.CountryId.Length > 0 ? new Guid(model.CountryId) : default(Guid)),
                            IsActive = model.IsActive,
                            UpdateBy = model.CurrentUserId,
                            UpdateDate = DateTime.Now,
                        }).ConfigureAwait(true);
                }
                else
                {
                    await _context.CustomerStoreRepository.AddAsync(new CustomerStore()
                    {
                        Id = Guid.NewGuid(),
                        Name = model.Name,
                        PrimaryPhone = model.PrimaryPhone,
                        SecondaryPhone = model.SecondaryPhone,
                        Fax = model.Fax,
                        StoreManagerId = (model.StoreManagerId.Length > 0 ? new Guid(model.StoreManagerId) : default(Guid)),
                        StartOn = model.StartOn.HasValue ? model.StartOn.Value.ToLocalTime() : model.StartOn,
                        Description = model.Description,
                        Address = model.Address,
                        Longtitue = model.Longtitue,
                        Latitude = model.Latitude,
                        CityId = (model.CityId.Length > 0 ? new Guid(model.CityId) : default(Guid)),
                        CountryId = (model.CountryId.Length > 0 ? new Guid(model.CountryId) : default(Guid)),
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
        /// Update customer store status function.
        /// </summary>
        /// <param name="model">Customer store model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> UpdateActiveStatus(CustomerStoreModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                Guid id = new Guid(model.Id);

                var checkExists = await _context.CustomerStoreRepository
                                                            .AnyAsync(m => m.Id == id
                                                                           && m.CreateBy == model.CurrentUserId)
                                                            .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                await _context.CustomerStoreRepository.Query().Where(m => m.Id == id)
                                                        .UpdateAsync(m => new CustomerStore()
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
        /// Delete customer store function.
        /// </summary>
        /// <param name="model">Customer store model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Delete(CustomerStoreModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                Guid id = new Guid(model.Id);

                var checkExists = await _context.CustomerRepository
                                                        .AnyAsync(m => m.Id == id
                                                                       && m.CreateBy == model.CurrentUserId)
                                                        .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                await _context.CustomerStoreRepository.Query()
                                                       .Where(m => m.Id == id)
                                                       .UpdateAsync(m => new CustomerStore()
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
