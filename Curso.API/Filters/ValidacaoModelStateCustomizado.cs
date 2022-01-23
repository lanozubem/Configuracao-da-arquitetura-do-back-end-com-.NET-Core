using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace curso.api.Models
{
    public class ValidacaoModelStateCustomizado : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validaCampoViewModel = new ValidaCampoViewModeOutput();
                context.Result = new BadRequestObjectResult(validaCampoViewModel);
            }
        }
    }
}
