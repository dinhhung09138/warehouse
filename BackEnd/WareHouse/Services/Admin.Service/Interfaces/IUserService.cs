using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Models;

namespace Admin.Service.Interfaces
{
    /// <summary>
    /// User interface.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get list of user function.
        /// </summary>
        /// <param name="filter">Filter model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> List(FilterModel filter);

        /// <summary>
        /// Create new user account function.
        /// </summary>
        /// <param name="model">User model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Create(Models.UserModel model);

        /// <summary>
        /// Update user account status function.
        /// </summary>
        /// <param name="model">User model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UpdateActiveStatus(Models.UserModel model);

        /// <summary>
        /// Delete user account function.
        /// </summary>
        /// <param name="model">User model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Delete(Models.UserModel model);
    }
}
