using Admin.Service.Models;
using Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Service.Interfaces
{
    /// <summary>
    /// Department service interface.
    /// </summary>
    public interface IDepartmentService
    {
        /// <summary>
        /// Get list of department data.
        /// </summary>
        /// <param name="filter">Filter model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> List(FilterModel filter);
        /// <summary>
        /// Get list of department data to show on combobox.
        /// </summary>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> ListCombobox();
        /// <summary>
        /// Get department detail.
        /// </summary>
        /// <param name="id">Department's id.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Detail(Guid id);
        /// <summary>
        /// Save a department function.
        /// </summary>
        /// <param name="model">Department model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Save(DepartmentModel model);
        /// <summary>
        /// Update department status function.
        /// </summary>
        /// <param name="model">Department model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UpdateActiveStatus(DepartmentModel model);
        /// <summary>
        /// Delete department function.
        /// </summary>
        /// <param name="model">Department model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Delete(DepartmentModel model);
    }
}
