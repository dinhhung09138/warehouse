using System;
using System.Collections.Generic;
using System.Text;
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
    /// Goods Category controller.
    /// </summary>
    [Route("api/warehouse/goods-category")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GoodsCategoryController : BaseController
    {
        private readonly IGoodsCategoryService _categoryService;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="provider">Service provider interface.</param>
        /// <param name="categoryService">Goods category service interface.</param>
        public GoodsCategoryController(IServiceProvider provider, IGoodsCategoryService categoryService)
            : base(provider)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Get list of goods category.
        /// </summary>
        /// <param name="filter">Filter model context.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> List([FromBody] FilterModel filter)
        {
            var response = await _categoryService.List(filter).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get list of goods category to show on combobox.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("list-combobox")]
        public async Task<IActionResult> ListCombobox()
        {
            var response = await _categoryService.ListCombobox().ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get goods category by id.
        /// </summary>
        /// <param name="id">Category's id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail([FromRoute] string id)
        {
            var response = await _categoryService.Detail(new Guid(id)).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Save goods category.
        /// </summary>
        /// <param name="model">Goods category model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save([FromBody] GoodsCategoryModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _categoryService.Save(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Update active status goods unit.
        /// </summary>
        /// <param name="model">Goods category model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("update-active-status")]
        public async Task<IActionResult> UpdateActiveStatus([FromBody] GoodsCategoryModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _categoryService.UpdateActiveStatus(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Delete goods category.
        /// </summary>
        /// <param name="model">Goods category model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] GoodsCategoryModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _categoryService.Delete(model).ConfigureAwait(false);
            return Ok(response);
        }
    }
}
