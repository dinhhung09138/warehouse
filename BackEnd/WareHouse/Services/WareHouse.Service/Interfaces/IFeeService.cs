using Core.Common.Models;
using System;
using System.Threading.Tasks;
using WareHouse.Service.Models;

namespace WareHouse.Service.Interfaces
{
    /// <summary>
    /// Fee service interface.
    /// </summary>
    public interface IFeeService
    {
        /// <summary>
        /// Get list of country data.
        /// </summary>
        /// <param name="filter">Filter model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> List(FilterModel filter);

        /// <summary>
        /// Get list of fee data to show on combobox.
        /// </summary>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> ListCombobox();

        /// <summary>
        /// Get fee detail.
        /// </summary>
        /// <param name="id">Fee's id.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Detail(Guid id);

        /// <summary>
        /// Save a fee function.
        /// </summary>
        /// <param name="model">Fee model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Save(FeeModel model);

        /// <summary>
        /// Update fee status function.
        /// </summary>
        /// <param name="model">Fee model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UpdateActiveStatus(FeeModel model);

        /// <summary>
        /// Delete fee function.
        /// </summary>
        /// <param name="model">Fee model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Delete(FeeModel model);
    }
}
