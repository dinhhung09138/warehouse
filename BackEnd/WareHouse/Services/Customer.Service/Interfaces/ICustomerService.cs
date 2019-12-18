﻿using System;
using System.Threading.Tasks;
using Core.Common.Models;
using Customer.Service.Models;

namespace Customer.Service.Interfaces
{
    /// <summary>
    /// Customer service interface.
    /// </summary>
    public interface ICustomerService
    {
        /// <summary>
        /// Get list of customer unit data.
        /// </summary>
        /// <param name="filter">Filter model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> List(FilterModel filter);
        /// <summary>
        /// Get list of customer data to show on combobox.
        /// </summary>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> ListCombobox();
        /// <summary>
        /// Get customer detail.
        /// </summary>
        /// <param name="id">Customer's id.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Detail(Guid id);
        /// <summary>
        /// Save a customer function.
        /// </summary>
        /// <param name="model">Customer model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Save(CustomerModel model);

        /// <summary>
        /// Create User name for customer.
        /// </summary>
        /// <param name="model">Customer employee model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> CreateUserAccount(CustomerModel model);

        /// <summary>
        /// Update passowrd for customer.
        /// </summary>
        /// <param name="model">Customer model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UpdatePassword(CustomerModel model);
        /// <summary>
        /// Update customer status function.
        /// </summary>
        /// <param name="model">Customer model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UpdateActiveStatus(CustomerModel model);
        /// <summary>
        /// Delete customer function.
        /// </summary>
        /// <param name="model">Customer model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Delete(CustomerModel model);
    }
}
