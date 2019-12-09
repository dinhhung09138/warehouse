using System;
using System.Threading.Tasks;
using Core.Common.Models;
using WareHouse.Service.Models;

namespace WareHouse.Service.Interfaces
{
    /// <summary>
    /// Goods category service interface.
    /// </summary>
    public interface IGoodsCategoryService
    {
        /// <summary>
        /// Get list of goods category data.
        /// </summary>
        /// <param name="filter">Filter model</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> List(FilterModel filter);

        /// <summary>
        /// Get list of goods category data to show on combobox.
        /// </summary>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> ListCombobox();

        /// <summary>
        /// Get goods category detail.
        /// </summary>
        /// <param name="id">Goods category's id.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Detail(Guid id);

        /// <summary>
        /// Save a goods category function.
        /// </summary>
        /// <param name="model">Goods category model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Save(GoodsCategoryModel model);

        /// <summary>
        /// Update goods category status function.
        /// </summary>
        /// <param name="model">Goods category model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UpdateActiveStatus(GoodsCategoryModel model);

        /// <summary>
        /// Delete goods category function.
        /// </summary>
        /// <param name="model">Goods category model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Delete(GoodsCategoryModel model);
    }
}
