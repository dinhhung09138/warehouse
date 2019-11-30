using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WareHouseApplication.Validations
{
    /// <summary>
    /// Validation model attribute class.
    /// </summary>
    public class ValidationModelAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Onaction executing method.
        /// </summary>
        /// <param name="context">ActionExecutingContext object.</param>
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (context == null)
            {
                throw new Exception(Core.Common.Messages.CommonMessage.ContextInvalid);
            }

            if (!context.ModelState.IsValid)
            {
                var result = new ResponseModel();

                var errors = from m in context.ModelState.Values
                          where m.Errors.Count > 0
                          let er = string.Join(",", m.Errors.Select(t => t.ErrorMessage))
                          select er;

                result.Errors.AddRange(errors);

                context.Result = new OkObjectResult(result);
            }

            base.OnActionExecuting(context);
        }
    }
}
