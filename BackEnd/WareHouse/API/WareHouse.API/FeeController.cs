using System;
using System.Threading.Tasks;
using Core.Common.Models;
using Microsoft.AspNetCore.Mvc;
using WareHouse.Service.Interfaces;
using Common.API;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using WareHouse.Service.Models;

namespace WareHouse.API
{
    /// <summary>
    /// Fee controller.
    /// </summary>
    [Route("api/warehouse/fee")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FeeController : BaseController
    {
        private readonly IFeeService _feeService;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="provider">Service provider interface.</param>
        /// <param name="goodsService">Fee service interface.</param>
        public FeeController(IServiceProvider provider, IFeeService feeService)
            : base(provider)
        {
            _feeService =feeService;
        }

        /// <summary>
        /// Get list of fee.
        /// </summary>
        /// <param name="filter">Filter model context.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> List([FromBody] FilterModel filter)
        {
            var response = await _feeService.List(filter).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get list of fee to show on combobox.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("list-combobox")]
        public async Task<IActionResult> ListCombobox()
        {
            var response = await _feeService.ListCombobox().ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get fee by id.
        /// </summary>
        /// <param name="id">Fee's id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail([FromRoute] string id)
        {
            var response = await _feeService.Detail(new Guid(id)).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Save fee.
        /// </summary>
        /// <param name="model">Fee model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save([FromBody] FeeModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _feeService.Save(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Update active status fee.
        /// </summary>
        /// <param name="model">Fee model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("update-active-status")]
        public async Task<IActionResult> UpdateActiveStatus([FromBody] FeeModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _feeService.UpdateActiveStatus(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Delete fee.
        /// </summary>
        /// <param name="model">Fee model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] FeeModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _feeService.Delete(model).ConfigureAwait(false);
            return Ok(response);
        }
    }
}
