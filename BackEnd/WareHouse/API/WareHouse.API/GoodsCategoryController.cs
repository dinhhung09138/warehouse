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
        public async Task<IActionResult> List(FilterModel filter)
        {
            var response = await _categoryService.List(filter).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Save goods category.
        /// </summary>
        /// <param name="model">Goods category model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save(GoodsCategoryModel model)
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
        public async Task<IActionResult> UpdateActiveStatus(GoodsCategoryModel model)
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
        [HttpDelete]
        [Route("delete")]
        public async Task<IActionResult> Delete(GoodsCategoryModel model)
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
