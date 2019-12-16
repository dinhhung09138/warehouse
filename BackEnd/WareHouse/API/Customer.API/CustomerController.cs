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
    /// Customer controller.
    /// </summary>
    [Route("api/customer")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerController : BaseController
    {
        private readonly ICustomerService _customerService;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="provider">Service provider interface.</param>
        /// <param name="customerService">Customer service interface.</param>
        public CustomerController(IServiceProvider provider, ICustomerService customerService)
            : base(provider)
        {
            _customerService = customerService;
        }

        /// <summary>
        /// Get list of customer.
        /// </summary>
        /// <param name="filter">Filter model context.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> List(FilterModel filter)
        {
            var response = await _customerService.List(filter).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get list of customer to show on combobox.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("list-combobox")]
        public async Task<IActionResult> ListCombobox()
        {
            var response = await _customerService.ListCombobox().ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get customer by id.
        /// </summary>
        /// <param name="id">Customer's id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail([FromRoute] string id)
        {
            var response = await _customerService.Detail(new Guid(id)).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Save customer.
        /// </summary>
        /// <param name="model">Customer model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(CustomerModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _customerService.Save(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Update active status customer.
        /// </summary>
        /// <param name="model">Customer model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("update-active-status")]
        public async Task<IActionResult> UpdateActiveStatus(CustomerModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _customerService.UpdateActiveStatus(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Delete customer.
        /// </summary>
        /// <param name="model">Customer model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> Delete(CustomerModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _customerService.Delete(model).ConfigureAwait(false);
            return Ok(response);
        }
    }
}
