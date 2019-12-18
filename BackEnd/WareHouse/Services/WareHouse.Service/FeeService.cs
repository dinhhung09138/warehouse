using Core.Common.Extensions;
using Core.Common.Messages;
using Core.Common.Models;
using Core.Common.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Warehouse.DataAccess;
using Warehouse.DataAccess.Entities;
using WareHouse.Service.Interfaces;
using WareHouse.Service.Models;
using Z.EntityFramework.Plus;

namespace WareHouse.Service
{
    /// <summary>
    /// Fee service.
    /// </summary>
    public class FeeService : IFeeService
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
        public FeeService(IWareHouseUnitOfWork context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get list of country data.
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

                var query = _context.FeeRepository.Query()
                                                   .Where(m => m.Deleted == false)
                                                   .Select(m => new FeeModel
                                                   {
                                                       Id = m.Id.ToString(),
                                                       Name = m.Name,
                                                       Value = m.Value,
                                                       IsActive = m.IsActive,
                                                       RowVersion = m.RowVersion,
                                                   });

                if (filter.Text.Length > 0)
                {
                    query = query.Where(m => m.Name.ToLower().Contains(filter.Text));
                }

                query = query.OrderBy(m => m.Name);

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
        /// Get list of fee data to show on combobox.
        /// </summary>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> ListCombobox()
        {
            var response = new ResponseModel();
            try
            {
                var query = _context.FeeRepository.Query()
                                                   .Where(m => m.Deleted == false && m.IsActive == true)
                                                   .Select(m => new SelectedItemModel
                                                   {
                                                       Value = m.Id.ToString(),
                                                       Title = $"{m.Name} - {m.Value}",
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
        /// Get fee detail.
        /// </summary>
        /// <param name="id">Fee's id.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Detail(Guid id)
        {
            var response = new ResponseModel();
            try
            {
                var item = await _context.FeeRepository.FirstOrDefaultAsync(m => m.Deleted == false
                                                                                 && m.Id == id)
                                                            .ConfigureAwait(false);

                if (item == null)
                {
                    throw new Exception(CommonMessage.IdNotFound);
                }

                FeeModel md = new FeeModel();
                md.Id = item.Id.ToString();
                md.Name = item.Name;
                md.Value = item.Value;
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
        /// Save a fee function.
        /// </summary>
        /// <param name="model">Fee model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Save(FeeModel model)
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

                    var checkExists = await _context.FeeRepository
                                                        .AnyAsync(m => m.Id == id)
                                                        .ConfigureAwait(false);

                    if (!checkExists)
                    {
                        response.Errors.Add(CommonMessage.IdNotFound);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    var checkCurrent = await _context.FeeRepository
                                                        .AnyAsync(m => m.Id == id
                                                                       && m.RowVersion != model.RowVersion)
                                                        .ConfigureAwait(false);

                    if (checkCurrent)
                    {
                        response.Errors.Add(CommonMessage.DataUpdatedByOtherUser);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    await _context.FeeRepository.Query()
                        .Where(m => m.Id == id)
                        .UpdateAsync(m => new Fee()
                        {
                            Name = model.Name,
                            Value = model.Value,
                            IsActive = model.IsActive,
                            UpdateBy = model.CurrentUserId,
                            UpdateDate = DateTime.Now,
                        }).ConfigureAwait(true);
                }
                else
                {
                    await _context.FeeRepository.AddAsync(new Fee()
                    {
                        Id = Guid.NewGuid(),
                        Name = model.Name,
                        Value = model.Value,
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
        /// Update fee status function.
        /// </summary>
        /// <param name="model">Fee model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> UpdateActiveStatus(FeeModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                Guid id = new Guid(model.Id);

                var checkExists = await _context.FeeRepository
                                                    .AnyAsync(m => m.Id == id)
                                                    .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                await _context.FeeRepository.Query()
                                            .Where(m => m.Id == id)
                                            .UpdateAsync(m => new Fee()
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
        /// Delete fee function.
        /// </summary>
        /// <param name="model">Fee model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Delete(FeeModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                Guid id = new Guid(model.Id);

                var checkExists = await _context.FeeRepository
                                                        .AnyAsync(m => m.Id == id)
                                                        .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                await _context.FeeRepository.Query()
                                        .Where(m => m.Id == id)
                                        .UpdateAsync(m => new Fee()
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
