using Admin.Service.Models;
using Core.Common.Models;
using System;
using System.Threading.Tasks;

namespace Admin.Service.Interfaces
{
    /// <summary>
    /// City service interface.
    /// </summary>
    public interface ICityService
    {
        /// <summary>
        /// Get list of city data.
        /// </summary>
        /// <param name="filter">Filter model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> List(FilterModel filter);


        /// <summary>
        /// Get list of city data to show on combobox by country's id.
        /// </summary>
        /// <param name="countryId">Country's id</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> ListCityByCountryId(Guid countryId);

        /// <summary>
        /// Get list of city data to show on combobox.
        /// </summary>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> ListCombobox();

        /// <summary>
        /// Get city detail.
        /// </summary>
        /// <param name="id">City's id.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Detail(Guid id);

        /// <summary>
        /// Save a city function.
        /// </summary>
        /// <param name="model">City model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Save(CityModel model);

        /// <summary>
        /// Update city status function.
        /// </summary>
        /// <param name="model">City model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UpdateActiveStatus(CityModel model);

        /// <summary>
        /// Delete city function.
        /// </summary>
        /// <param name="model">City model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Delete(CityModel model);
    }
}
