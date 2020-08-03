using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace BeltsAndLeaders.Server.Api.Controllers
{
    public class BaseController : Controller
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult("Request path or body parameter(s) missing or invalid.");
            }

            base.OnActionExecuting(context);
        }
    }
}
