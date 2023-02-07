using Lms.Core.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lms.Api.Filters
{
    public class ValidateModelStateAttribute : Attribute, IActionFilter
    {
        readonly IUnitOfWork _uow;

        public ValidateModelStateAttribute(IUnitOfWork uow)
        {
            _uow = uow;
            ArgumentNullException.ThrowIfNull(_uow.TournamentRepository);
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ActionArguments.Values.Contains(null))
            {
                context.Result = new BadRequestResult();
            }
            if (!context.ModelState.IsValid)
            {
                context.Result = new UnprocessableEntityObjectResult(context.ModelState);
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
