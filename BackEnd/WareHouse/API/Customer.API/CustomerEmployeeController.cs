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
    [Route("api/customer/employee")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CustomerEmployeeController : BaseController
    {
        private readonly ICustomerEmployeeService _customerEmplService;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="provider">Service provider interface.</param>
        /// <param name="customerEmployeeService">Customer employee service interface.</param>
        public CustomerEmployeeController(IServiceProvider provider, ICustomerEmployeeService customerEmployeeService)
            : base(provider)
        {
            _customerEmplService = customerEmployeeService;
        }

        /// <summary>
        /// Get list of customer's employee.
        /// </summary>
        /// <param name="filter">Filter model context.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> List(CustomerFilterModel filter)
        {
            filter.ClientId = base.CurrentClientId();
            var response = await _customerEmplService.List(filter).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get list of customer's employee to show on combobox.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("list-combobox")]
        public async Task<IActionResult> ListCombobox()
        {
            var response = await _customerEmplService.ListCombobox(base.CurrentClientId()).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get customer's employee by id.
        /// </summary>
        /// <param name="id">Customer employee's id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail([FromRoute] string id)
        {
            var response = await _customerEmplService.Detail(new Guid(id), base.CurrentClientId()).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Save customer's employee.
        /// </summary>
        /// <param name="model">Customer's employee model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save([FromForm] CustomerEmployeeModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
                model.ClientId = base.CurrentClientId();
            }

            var response = await _customerEmplService.Save(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Create customer's employee account login.
        /// </summary>
        /// <param name="model">Customer's employee model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("create-account")]
        public async Task<IActionResult> CreateUserAccount(CustomerEmployeeModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
                model.ClientId = base.CurrentClientId();
            }

            var response = await _customerEmplService.CreateUserAccount(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Update password.
        /// </summary>
        /// <param name="model">Customer's employee model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("update-password")]
        public async Task<IActionResult> UpdatePassword(CustomerEmployeeModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
                model.ClientId = base.CurrentClientId();
            }

            var response = await _customerEmplService.UpdatePassword(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Update active status customer's employee.
        /// </summary>
        /// <param name="model">Customer's employee model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("update-active-status")]
        public async Task<IActionResult> UpdateActiveStatus(CustomerEmployeeModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
                model.ClientId = base.CurrentClientId();
            }

            var response = await _customerEmplService.UpdateActiveStatus(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Delete customer's employee.
        /// </summary>
        /// <param name="model">Customer's employee model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> Delete(CustomerEmployeeModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
                model.ClientId = base.CurrentClientId();
            }

            var response = await _customerEmplService.Delete(model).ConfigureAwait(false);
            return Ok(response);
        }
    }
}
