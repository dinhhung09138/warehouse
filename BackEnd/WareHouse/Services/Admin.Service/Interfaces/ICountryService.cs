using Admin.Service.Models;
using Core.Common.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Service.Interfaces
{
    /// <summary>
    /// Country service interface.
    /// </summary>
    public interface ICountryService
    {
        /// <summary>
        /// Get list of country data.
        /// </summary>
        /// <param name="filter">Filter model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> List(FilterModel filter);

        /// <summary>
        /// Get list of country data to show on combobox.
        /// </summary>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> ListCombobox();

        /// <summary>
        /// Get country detail.
        /// </summary>
        /// <param name="id">Country's id.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Detail(Guid id);

        /// <summary>
        /// Save a country function.
        /// </summary>
        /// <param name="model">Country model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Save(CountryModel model);

        /// <summary>
        /// Update country status function.
        /// </summary>
        /// <param name="model">Country model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> UpdateActiveStatus(CountryModel model);

        /// <summary>
        /// Delete country function.
        /// </summary>
        /// <param name="model">Country model.</param>
        /// <returns>ResponseModel object.</returns>
        Task<ResponseModel> Delete(CountryModel model);
    }
}
