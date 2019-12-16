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
    [Route("api/admin/department")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class DepartmentController : BaseController
    {
        private readonly IDepartmentService _departmentService;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="provider">Service provider interface.</param>
        /// <param name="departmentService">Department service interface.</param>
        public DepartmentController(IServiceProvider provider, IDepartmentService departmentService)
            : base(provider)
        {
            _departmentService = departmentService;
        }

        /// <summary>
        /// Get list of department.
        /// </summary>
        /// <param name="filter">Filter model context.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> List([FromBody] FilterModel filter)
        {
            var response = await _departmentService.List(filter).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get list of department to show on combobox.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("list-combobox")]
        public async Task<IActionResult> ListCombobox()
        {
            var response = await _departmentService.ListCombobox().ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get department by id.
        /// </summary>
        /// <param name="id">Department's id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail([FromRoute] string id)
        {
            var response = await _departmentService.Detail(new Guid(id)).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Save department.
        /// </summary>
        /// <param name="model">Department model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(DepartmentModel model)
        {

            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _departmentService.Save(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Update active status of department.
        /// </summary>
        /// <param name="model">Department model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("update-active-status")]
        public async Task<IActionResult> UpdateActiveStatus([FromBody] DepartmentModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _departmentService.UpdateActiveStatus(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Delete department.
        /// </summary>
        /// <param name="model">Department model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] DepartmentModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _departmentService.Delete(model).ConfigureAwait(false);
            return Ok(response);
        }
    }
}
