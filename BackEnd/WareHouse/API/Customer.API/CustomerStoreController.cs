using System;
using System.Threading.Tasks;
using Core.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Customer.Service.Interfaces;
using Common.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Customer.Service.Models;

namespace Customer.API
{
    /// <summary>
    /// Customer store controller.
    /// </summary>
    [Route("api/customer/store")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerStoreController : BaseController
    {
        private readonly ICustomerStoreService _customerStoreService;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="provider">Service provider interface.</param>
        /// <param name="customerService">Customer store service interface.</param>
        public CustomerStoreController(IServiceProvider provider, ICustomerStoreService customerStoreService)
            : base(provider)
        {
            _customerStoreService = customerStoreService;
        }

        /// <summary>
        /// Get list of customer's store.
        /// </summary>
        /// <param name="filter">Filter model context.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> List(CustomerFilterModel filter)
        {
            var response = await _customerStoreService.List(filter).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get list of customer's store to show on combobox.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("list-combobox")]
        public async Task<IActionResult> ListCombobox()
        {
            var response = await _customerStoreService.ListCombobox(base.CurrentUserId()).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get customer's store by id.
        /// </summary>
        /// <param name="id">Customer store's id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail([FromRoute] string id)
        {
            var response = await _customerStoreService.Detail(new Guid(id), base.CurrentUserId()).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Save customer's store.
        /// </summary>
        /// <param name="model">Customer's store model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(CustomerStoreModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _customerStoreService.Save(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Update active status customer's store.
        /// </summary>
        /// <param name="model">Customer's store model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("update-active-status")]
        public async Task<IActionResult> UpdateActiveStatus(CustomerStoreModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _customerStoreService.UpdateActiveStatus(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Delete customer's store.
        /// </summary>
        /// <param name="model">Customer's store model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> Delete(CustomerStoreModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _customerStoreService.Delete(model).ConfigureAwait(false);
            return Ok(response);
        }
    }
}
