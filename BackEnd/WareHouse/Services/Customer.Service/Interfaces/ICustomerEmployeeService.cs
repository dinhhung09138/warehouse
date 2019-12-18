using Core.Common.Models;
using Customer.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Service.Interfaces
{
    /// <summary>
    /// Customer Employee service interface.
    /// </summary>
    public interface ICustomerEmployeeService
    {
        /// <summary>
        /// Get list of customer employee data.
        /// </summary>
        /// <param name="filter">Filter model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> List(CustomerFilterModel filter);

        /// <summary>
        /// Get list of customer employee data to show on combobox.
        /// </summary>
        /// <param name="customerId">Customer's id</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> ListCombobox(string customerId);

        /// <summary>
        /// Get customer employee detail.
        /// </summary>
        /// <param name="id">Customer employee's id.</param>
        /// <param name="customerId">Customer's id</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Detail(Guid id, string customerId);

        /// <summary>
        /// Save a customer employee function.
        /// </summary>
        /// <param name="model">Customer employee model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Save(CustomerEmployeeModel model);

        /// <summary>
        /// Update customer employee status function.
        /// </summary>
        /// <param name="model">Customer employee model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UpdateActiveStatus(CustomerEmployeeModel model);

        /// <summary>
        /// Create User name for employee.
        /// </summary>
        /// <param name="model">Customer employee model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> CreateUserAccount(CustomerEmployeeModel model);

        /// <summary>
        /// Update passowrd for employee.
        /// </summary>
        /// <param name="model">Customer employee model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UpdatePassword(CustomerEmployeeModel model);

        /// <summary>
        /// Delete customer employee function.
        /// </summary>
        /// <param name="model">Customer employee model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Delete(CustomerEmployeeModel model);
    }
}
