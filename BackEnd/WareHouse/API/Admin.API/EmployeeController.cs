using Admin.Service.Interfaces;
using Admin.Service.Models;
using Common.API;
using Core.Common.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Admin.API
{
    /// <summary>
    /// Goods controller.
    /// </summary>
    [Route("api/admin/employee")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class EmployeeController : BaseController
    {
        private readonly IEmployeeService _employeeService;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="provider">Service provider interface.</param>
        /// <param name="employeeService">Employee service interface.</param>
        public EmployeeController(IServiceProvider provider, IEmployeeService employeeService)
            : base(provider)
        {
            _employeeService = employeeService;
        }

        /// <summary>
        /// Get list of employee.
        /// </summary>
        /// <param name="filter">Filter model context.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> List([FromBody] FilterModel filter)
        {
            var response = await _employeeService.List(filter).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get employee by id.
        /// </summary>
        /// <param name="id">employee's id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail([FromRoute] string id)
        {
            var response = await _employeeService.Detail(new Guid(id)).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Save employee.
        /// </summary>
        /// <param name="model">employee model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(EmployeeModel model)
        {
          
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _employeeService.Save(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Update active status employee.
        /// </summary>
        /// <param name="model">Employee model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("update-active-status")]
        public async Task<IActionResult> UpdateActiveStatus([FromBody] EmployeeModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _employeeService.UpdateActiveStatus(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Delete employee.
        /// </summary>
        /// <param name="model">Employee model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] EmployeeModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _employeeService.Delete(model).ConfigureAwait(false);
            return Ok(response);
        }
    }
}
