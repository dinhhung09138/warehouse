using Admin.Service.Models;
using Core.Common.Models;
using System;
using System.Threading.Tasks;

namespace Admin.Service.Interfaces
{
    /// <summary>
    /// Employee service interface.
    /// </summary>
    public interface IEmployeeService
    {
        /// <summary>
        /// Get list of employee unit data.
        /// </summary>
        /// <param name="filter">Filter model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> List(FilterModel filter);
        /// <summary>
        /// Get list of employee data to show on combobox.
        /// </summary>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> ListCombobox();
        /// <summary>
        /// Get employee detail.
        /// </summary>
        /// <param name="id">Employee's id.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Detail(Guid id);
        /// <summary>
        /// Save a employee function.
        /// </summary>
        /// <param name="model">Employee model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Save(EmployeeModel model);
        /// <summary>
        /// Update employee status function.
        /// </summary>
        /// <param name="model">Employee model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UpdateActiveStatus(EmployeeModel model);
        /// <summary>
        /// Delete employee function.
        /// </summary>
        /// <param name="model">Employee model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Delete(EmployeeModel model);
    }
}
