using Lms.Core.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Lms.Api.Filters
{
    public class ValidateModelStateAttribute : Attribute, IActionFilter
    {
        IUnitOfWork _uow;

        public ValidateModelStateAttribute(IUnitOfWork uow)
        {
            _uow = uow;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (_uow.TournamentRepository == null)
            {
                context.Result= new NotFoundResult();
            }
            if (context.ActionArguments.Values.Contains(null))
            {
                context.Result = new BadRequestResult();
            }
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
    }
}
