﻿using System;
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
    /// Goods Unit controller.
    /// </summary>
    [Route("api/warehouse/goods-unit")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GoodsUnitController : BaseController
    {
        private readonly IGoodsUnitService _unitService;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="provider">Service provider interface.</param>
        /// <param name="unitService">Goods unit service interface.</param>
        public GoodsUnitController(IServiceProvider provider, IGoodsUnitService unitService)
            : base(provider)
        {
            _unitService = unitService;
        }

        /// <summary>
        /// Get list of goods unit.
        /// </summary>
        /// <param name="filter">Filter model context.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("list")]
        public async Task<IActionResult> List([FromBody] FilterModel filter)
        {
            var response = await _unitService.List(filter).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get list of goods unit to show on combobox.
        /// </summary>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("list-combobox")]
        public async Task<IActionResult> ListCombobox()
        {
            var response = await _unitService.ListCombobox().ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Get goods unit by id.
        /// </summary>
        /// <param name="id">Unit's id.</param>
        /// <returns>IActionResult.</returns>
        [HttpGet]
        [Route("detail/{id}")]
        public async Task<IActionResult> Detail([FromRoute] string id)
        {
            var response = await _unitService.Detail(new Guid(id)).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Save goods unit.
        /// </summary>
        /// <param name="model">Goods unit model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPost]
        [Route("save")]
        public async Task<IActionResult> Save([FromBody] GoodsUnitModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _unitService.Save(model).ConfigureAwait(false);
            return Ok(response);
        }

        /// <summary>
        /// Delete goods unit.
        /// </summary>
        /// <param name="model">Goods unit model.</param>
        /// <returns>IActionResult.</returns>
        [HttpPut]
        [Route("delete")]
        public async Task<IActionResult> Delete([FromBody] GoodsUnitModel model)
        {
            if (model != null)
            {
                model.CurrentUserId = CurrentUserId();
            }

            var response = await _unitService.Delete(model).ConfigureAwait(false);
            return Ok(response);
        }
    }
}
