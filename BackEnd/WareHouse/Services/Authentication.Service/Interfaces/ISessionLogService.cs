using System.Threading.Tasks;
using Core.Common.Models;
using Authentication.Service.Models;

namespace Authentication.Service.Interfaces
{

    /// <summary>
    /// Session log service interface.
    /// </summary>
    public interface ISessionLogService
    {
        /// <summary>
        /// Add new session log.
        /// </summary>
        /// <param name="model">Session log model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Add(SessionLogModel model);

        /// <summary>
        /// Add new session log.
        /// </summary>
        /// <param name="model">token model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Add(JwtTokenModel model);

        /// <summary>
        /// Add new session log.
        /// </summary>
        /// <param name="tokenModel">token model.</param>
        /// <param name="loginModel">Login model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Add(JwtTokenModel tokenModel, LoginModel loginModel);
    }
}
