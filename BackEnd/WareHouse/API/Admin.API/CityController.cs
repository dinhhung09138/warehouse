using Admin.Service.Interfaces;
using Admin.Service.Models;
using Common.API;
using Core.Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Admin.API
{
    /// <summary>
    /// Department controller.
    /// </summary>
    [Route("api/admin/city")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CityController : BaseController
    {
        private readonly ICityService _cityService;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="provider">Service provider interface.</param>
        /// <param name="cityService">City service interface.</param>
        public CityController(IServiceProvider provider, ICityService cityService)
            : base(provider)
        {
            _cityService = cityService;
        }

        /// <summary>
        /// Get list of city.
        /// </summary>
        /// <param name="filter">Filter model context.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> List([FromBody] FilterModel filter)
        {
            var response = await _cityService.List(filter).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get list of city to show on combobox.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("list-combobox")]
        public async Task<IActionResult> ListCombobox()
        {
            var response = await _cityService.ListCombobox().ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get list of city to show on combobox by country's id.
        /// </summary>
        /// <param name="countryId">Country's id</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("list-city-by-country/{countryId}")]
        public async Task<IActionResult> ListCityByCountryId(string countryId)
        {
            var response = await _cityService.ListCityByCountryId(new Guid(countryId)).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get city by id.
        /// </summary>
        /// <param name="id">City's id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail([FromRoute] string id)
        {
            var response = await _cityService.Detail(new Guid(id)).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Save city.
        /// </summary>
        /// <param name="model">City model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(CityModel model)
        {

            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _cityService.Save(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Update active status of city.
        /// </summary>
        /// <param name="model">City model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("update-active-status")]
        public async Task<IActionResult> UpdateActiveStatus([FromBody] CityModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _cityService.UpdateActiveStatus(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Delete city.
        /// </summary>
        /// <param name="model">City model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] CityModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _cityService.Delete(model).ConfigureAwait(false);
            return Ok(response);
        }
    }
}
