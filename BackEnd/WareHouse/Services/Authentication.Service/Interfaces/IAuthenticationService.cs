using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Common.Models;

namespace Authentication.Service.Interfaces
{
    /// <summary>
    /// Authentication service interface.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Login function.
        /// </summary>
        /// <param name="model">login model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Login(LoginModel model);
    }
}
