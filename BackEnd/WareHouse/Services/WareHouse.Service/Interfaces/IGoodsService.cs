using System;
using System.Threading.Tasks;
using Core.Common.Models;
using WareHouse.Service.Models;

namespace WareHouse.Service.Interfaces
{
    /// <summary>
    /// Goods service interface.
    /// </summary>
    public interface IGoodsService
    {
        /// <summary>
        /// Get list of goods data.
        /// </summary>
        /// <param name="filter">Filter model</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> List(FilterModel filter);

        /// <summary>
        /// Get goods detail.
        /// </summary>
        /// <param name="id">Goods's id.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Detail(Guid id);

        /// <summary>
        /// Save a goods function.
        /// </summary>
        /// <param name="model">Goods model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Save(GoodsModel model);

        /// <summary>
        /// Update goods status function.
        /// </summary>
        /// <param name="model">Goods model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UpdateActiveStatus(GoodsModel model);

        /// <summary>
        /// Delete goods function.
        /// </summary>
        /// <param name="model">Goods model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Delete(GoodsModel model);
    }
}
