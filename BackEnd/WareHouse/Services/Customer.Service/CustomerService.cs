using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Customer.Service.Interfaces;
using Customer.Service.Models;
using Core.Common.Extensions;
using Core.Common.Messages;
using Core.Common.Models;
using Core.Common.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Warehouse.DataAccess;
using Z.EntityFramework.Plus;
using Core.Common.Helpers;
using Customer.Service.Constants;

namespace Customer.Service
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
        /// <param name="context">Data context.</param>
        /// <param name="logger">Log service.</param>
        public CustomerService(IWareHouseUnitOfWork context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
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

                var query = from cus in _context.CustomerRepository.Query()
                            join country in _context.CountryRepository.Query() on cus.CountryId equals country.Id
                            into countries
                            from country in countries.DefaultIfEmpty()
                            join city in _context.CityRepository.Query() on cus.CityId equals city.Id
                            into cities
                            from city in cities.DefaultIfEmpty()
                            where cus.Deleted == false
                            select new CustomerModel
                            {
                                Id = cus.Id.ToString(),
                                Name = cus.Name,
                                PrimaryPhone = cus.PrimaryPhone,
                                Fax = cus.Fax,
                                Email = cus.Email,
                                Website = cus.Website,
                                IsCompany = cus.IsCompany,
                                ContactName = cus.ContactName,
                                ContactPhone = cus.ContactPhone,
                                ContactEmail = cus.ContactEmail,
                                StartOn = cus.StartOn,
                                Address = cus.Address,
                                IsActive = cus.IsActive,
                                CountryId = country.Id != null ? country.Id.ToString() : string.Empty,
                                CountryName = country.Id != null ? country.Name.ToString() : string.Empty,
                                CityId = city.Id != null ? city.Id.ToString() : string.Empty,
                                CityName = city.Id != null ? city.Name.ToString() : string.Empty,
                            };

                if (filter.Text.Length > 0)
                {
                    query = query.Where(m => m.Name.ToLower().Contains(filter.Text)
                                            || m.PrimaryPhone.ToLower().Contains(filter.Text)
                                            || m.Fax.ToLower().Contains(filter.Text)
                                            || m.Website.ToLower().Contains(filter.Text)
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
        /// Get list of custoer data to show on combobox.
        /// </summary>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> ListCombobox()
        {
            var response = new ResponseModel();
            try
            {
                var query = _context.CustomerRepository.Query()
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
                md.Id = item.Id.ToString();
                md.Name = item.Name;
                md.LogoFileId = item.LogoFileId.HasValue ? string.Empty : item.LogoFileId.ToString();
                md.PrimaryPhone = item.PrimaryPhone;
                md.SecondaryPhone = item.SecondaryPhone;
                md.Fax = item.Fax;
                md.Website = item.Website;
                md.TaxCode = item.TaxCode;
                md.Email = item.Email;
                md.ContactName = item.ContactName;
                md.ContactPhone = item.ContactPhone;
                md.ContactEmail = item.ContactEmail;
                md.IsCompany = item.IsCompany;
                md.StartOn = item.StartOn;
                md.Description = item.Description;
                md.Address = item.Address;
                md.CityId = !item.CityId.HasValue ? string.Empty : item.CityId.ToString();
                md.CountryId = !item.CountryId.HasValue ? string.Empty : item.CountryId.ToString();
                md.Longtitue = item.Longtitue;
                md.Latitude = item.Latitude;
                md.UserName = item.UserName;
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
                    Guid id = new Guid(model.Id);

                    var checkExists = await _context.CustomerRepository
                                                        .AnyAsync(m => m.Id == id)
                                                        .ConfigureAwait(false);

                    if (!checkExists)
                    {
                        response.Errors.Add(CommonMessage.IdNotFound);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    var checkCurrent = await _context.CustomerRepository
                                                        .AnyAsync(m => m.Id == id
                                                                       && m.RowVersion != model.RowVersion)
                                                        .ConfigureAwait(false);

                    if (checkCurrent)
                    {
                        response.Errors.Add(CommonMessage.DataUpdatedByOtherUser);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    await _context.CustomerRepository.Query()
                        .Where(m => m.Id == id)
                        .UpdateAsync(m => new Warehouse.DataAccess.Entities.Customer()
                        {
                            Name = model.Name,
                            LogoFileId = null, // TOOD
                            PrimaryPhone = model.PrimaryPhone,
                            SecondaryPhone = model.SecondaryPhone,
                            Fax = model.Fax,
                            Website = model.Website,
                            TaxCode = model.TaxCode,
                            Email = model.Email,
                            IsCompany = model.IsCompany,
                            StartOn = model.StartOn.HasValue ? model.StartOn.Value.ToLocalTime() : model.StartOn,
                            Description = model.Description,
                            Address = model.Address,
                            Longtitue = model.Longtitue,
                            Latitude = model.Latitude,
                            ContactName = model.ContactName,
                            ContactPhone = model.ContactPhone,
                            ContactEmail = model.ContactEmail,
                            CityId = (model.CityId.Length > 0 ? new Guid(model.CityId) : default(Guid)),
                            CountryId = (model.CountryId.Length > 0 ? new Guid(model.CountryId) : default(Guid)),
                            IsActive = model.IsActive,
                            UpdateBy = model.CurrentUserId,
                            UpdateDate = DateTime.Now,
                        }).ConfigureAwait(true);
                }
                else
                {
                    string password = string.Empty;

                    if (!string.IsNullOrEmpty(model.UserName))
                    {
                        password = PasswordSecurityHelper.GetHashedPassword(model.Password);
                    }

                    await _context.CustomerRepository.AddAsync(new Warehouse.DataAccess.Entities.Customer()
                    {
                        Id = Guid.NewGuid(),
                        Name = model.Name,
                        LogoFileId = null, // TOOD
                        PrimaryPhone = model.PrimaryPhone,
                        SecondaryPhone = model.SecondaryPhone,
                        Fax = model.Fax,
                        Website = model.Website,
                        TaxCode = model.TaxCode,
                        Email = model.Email,
                        IsCompany = model.IsCompany,
                        StartOn = model.StartOn.HasValue ? model.StartOn.Value.ToLocalTime() : model.StartOn,
                        Description = model.Description,
                        Address = model.Address,
                        Longtitue = model.Longtitue,
                        Latitude = model.Latitude,
                        ContactName = model.ContactName,
                        ContactPhone = model.ContactPhone,
                        ContactEmail = model.ContactEmail,
                        CityId = (model.CityId.Length > 0 ? new Guid(model.CityId) : default(Guid)),
                        CountryId = (model.CountryId.Length > 0 ? new Guid(model.CountryId) : default(Guid)),
                        UserName = model.UserName,
                        Password = password,
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
        /// Create User name for customer.
        /// </summary>
        /// <param name="model">Customer employee model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> CreateUserAccount(CustomerModel model)
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

                await _context.CustomerRepository.Query().Where(m => m.Id == id)
                                                        .UpdateAsync(m => new Warehouse.DataAccess.Entities.Customer()
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
        /// Update passowrd for customer.
        /// </summary>
        /// <param name="model">Customer model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> UpdatePassword(CustomerModel model)
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

                await _context.CustomerRepository.Query().Where(m => m.Id == id)
                                                        .UpdateAsync(m => new Warehouse.DataAccess.Entities.Customer()
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

                Guid id = new Guid(model.Id);

                var checkExists = await _context.CustomerRepository
                                                            .AnyAsync(m => m.Id == id)
                                                            .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                await _context.CustomerRepository.Query()
                                                    .Where(m => m.Id == id)
                                                    .UpdateAsync(m => new Warehouse.DataAccess.Entities.Customer()
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

                Guid id = new Guid(model.Id);

                var checkExists = await _context.CustomerRepository
                                                        .AnyAsync(m => m.Id == id)
                                                        .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                await _context.CustomerRepository.Query()
                                                .Where(m => m.Id == id)
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
            catch (Exception ex)
            {
                _logger.AddErrorLog(this.GetType().Name, MethodBase.GetCurrentMethod().Name, model, ex);
                response.ResponseStatus = Core.Common.Enums.ResponseStatus.Error;
            }

            return response;
        }
    }
}
