using Core.Common.Models;
using Customer.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Service.Interfaces
{
    /// <summary>
    /// Customer store service interface.
    /// </summary>
    public interface ICustomerStoreService
    {
        /// <summary>
        /// Get list of customer store data.
        /// </summary>
        /// <param name="filter">Filter model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> List(CustomerFilterModel filter);

        /// <summary>
        /// Get list of customer's store data to show on combobox.
        /// </summary>
        /// <param name="customerId">Customer's id</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> ListCombobox(string customerId);

        /// <summary>
        /// Get customer store detail.
        /// </summary>
        /// <param name="id">Customer store's id.</param>
        /// <param name="customerId">Customer's id</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Detail(Guid id, string customerId);

        /// <summary>
        /// Save a customer store function.
        /// </summary>
        /// <param name="model">Customer store model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Save(CustomerStoreModel model);

        /// <summary>
        /// Update customer store status function.
        /// </summary>
        /// <param name="model">Customer store model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UpdateActiveStatus(CustomerStoreModel model);

        /// <summary>
        /// Delete customer store function.
        /// </summary>
        /// <param name="model">Customer store model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Delete(CustomerStoreModel model);
    }
}
