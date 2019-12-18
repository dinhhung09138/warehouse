using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Core.Common.Extensions;
using Core.Common.Messages;
using Core.Common.Models;
using Core.Common.Services.Interfaces;
using WareHouse.Service.Interfaces;
using WareHouse.Service.Models;
using Warehouse.DataAccess;
using Warehouse.DataAccess.Entities;
using Z.EntityFramework.Plus;
using WareHouse.Service.Constants;
using Microsoft.EntityFrameworkCore;

namespace WareHouse.Service
{
    /// <summary>
    /// Goods service.
    /// </summary>
    public class GoodsService : IGoodsService
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
        public GoodsService(IWareHouseUnitOfWork context, ILoggerService logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Get list of goods data.
        /// </summary>
        /// <param name="filter">Filter model</param>
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

                var query = _context.GoodsRepository.Query()
                                                   .Where(m => m.Deleted == false)
                                                   .Select(m => new GoodsModel
                                                   {
                                                       Id = m.Id.ToString(),
                                                       Code = m.Code,
                                                       Name = m.Name,
                                                       Description = m.Description ?? string.Empty,
                                                       IsActive = m.IsActive,
                                                   });

                if (filter.Text.Length > 0)
                {
                    query = query.Where(m => m.Description.ToLower().Contains(filter.Text)
                                            || m.Name.ToLower().Contains(filter.Text)
                                            || m.Code.ToLower().Contains(filter.Text));
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
        /// Get list of goods data to show on combobox.
        /// </summary>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> ListCombobox()
        {
            var response = new ResponseModel();
            try
            {
                var query = _context.GoodsRepository.Query()
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
        /// Get goods detail.
        /// </summary>
        /// <param name="id">Goods's id.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Detail(Guid id)
        {
            var response = new ResponseModel();
            try
            {
                var item = await _context.GoodsRepository.FirstOrDefaultAsync(m => m.Deleted == false
                                                                                           && m.Id == id)
                                                            .ConfigureAwait(false);

                if (item == null)
                {
                    throw new Exception(CommonMessage.IdNotFound);
                }

                GoodsModel md = new GoodsModel();
                md.Id = id.ToString();
                md.Code = item.Code;
                md.Name = item.Name;
                md.Description = item.Description;
                md.Brand = item.Brand;
                md.Color = item.Color;
                md.Size = item.Size;
                md.FileId = item.FileId.HasValue ? item.FileId.ToString() : "";
                md.UnitId = item.UnitId.ToString();
                md.GoodsCategoryId = item.GoodsCategoryId.ToString();
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
        /// Save a goods function.
        /// </summary>
        /// <param name="model">Goods model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Save(GoodsModel model)
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

                    var checkExists = await _context.GoodsRepository
                                                        .AnyAsync(m => m.Id == id)
                                                        .ConfigureAwait(false);

                    if (!checkExists)
                    {
                        response.Errors.Add(CommonMessage.IdNotFound);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    var checkCurrent = await _context.GoodsRepository
                                                        .AnyAsync(m => m.Id == id
                                                                       && m.RowVersion != model.RowVersion)
                                                        .ConfigureAwait(false);

                    if (checkCurrent)
                    {
                        response.Errors.Add(CommonMessage.DataUpdatedByOtherUser);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    await _context.GoodsRepository.Query()
                        .Where(m => m.Id == id)
                        .UpdateAsync(m => new Goods()
                        {
                            Code = model.Code,
                            Name = model.Name,
                            Brand = model.Brand,
                            Color = model.Color,
                            Size = model.Size,
                            FileId = model.FileId.Length > 0 ? new Guid(model.FileId) : default(Guid),
                            UnitId = new Guid(model.UnitId),
                            GoodsCategoryId = new Guid(model.GoodsCategoryId),
                            Description = model.Description,
                            IsActive = model.IsActive,
                            UpdateBy = model.CurrentUserId,
                            UpdateDate = DateTime.Now,
                        }).ConfigureAwait(true);
                }
                else
                {
                    var checkCode = await _context.GoodsRepository.AnyAsync(m => m.Code == model.Code);
                    if (checkCode)
                    {
                        response.Errors.Add(Message.CodeIsExists);
                        response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                        return response;
                    }

                    await _context.GoodsRepository.AddAsync(new Goods()
                    {
                        Id = Guid.NewGuid(),
                        Code = model.Code,
                        Name = model.Name,
                        Brand = model.Brand,
                        Color = model.Color,
                        Size = model.Size,
                        //FileId = model.FileId.Length > 0 ? new Guid(model.FileId) : default(Guid),
                        UnitId = new Guid(model.UnitId),
                        GoodsCategoryId = new Guid(model.GoodsCategoryId),
                        Description = model.Description,
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
        /// Update goods status function.
        /// </summary>
        /// <param name="model">Goods model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> UpdateActiveStatus(GoodsModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                Guid id = new Guid(model.Id);

                var checkExists = await _context.GoodsRepository
                                                            .AnyAsync(m => m.Id == id)
                                                            .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.IdNotFound);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                await _context.GoodsRepository.Query()
                                                .Where(m => m.Id == id)
                                                .UpdateAsync(m => new Goods()
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
        /// Delete goods function.
        /// </summary>
        /// <param name="model">Goods model.</param>
        /// <returns>ResponseModel object.</returns>
        public async Task<ResponseModel> Delete(GoodsModel model)
        {
            var response = new ResponseModel();
            try
            {
                if (model == null)
                {
                    throw new Exception(CommonMessage.ParameterInvalid);
                }

                Guid id = new Guid(model.Id);

                var checkExists = await _context.GoodsRepository
                                                        .AnyAsync(m => m.Id == id)
                                                        .ConfigureAwait(true);

                if (!checkExists)
                {
                    response.Errors.Add(CommonMessage.ParameterInvalid);
                    response.ResponseStatus = Core.Common.Enums.ResponseStatus.Warning;
                    return response;
                }

                await _context.GoodsRepository.Query()
                                            .Where(m => m.Id == id)
                                            .UpdateAsync(m => new Goods()
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
