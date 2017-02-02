using System.Linq;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Squares.Contracts.Exceptions;

namespace Squares.WebApi.Filters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (actionContext.ActionArguments.Any(kv => kv.Value == null) && actionContext.Request.Method.Method != "GET")
            {
                throw new BadRequestException("Request format is invalid.", "invalidFormat");
            }
            if (!actionContext.ModelState.IsValid)
            {
                var a = actionContext.ModelState;
                throw new BadRequestException($"Requests is invalid. Please check these properties:{string.Join(",", actionContext.ModelState.Keys.ToList())}", "invalidProperties");
            }
        }
    }
}