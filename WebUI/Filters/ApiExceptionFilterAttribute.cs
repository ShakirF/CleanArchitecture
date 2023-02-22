using CleanArchitecture.Application.Common.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CleanArchitecture.WebUI.Filters;

    public class ApiExceptionFilterAttribute :ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        public ApiExceptionFilterAttribute()
        {
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                {typeof(ValidationException),HandleValidationException },
                {typeof(NotFoundExxeption),HandleNotFoundException }

            };
        }

        public override void OnException(ExceptionContext context)
        {
        this.HandleException(context);
            base.OnException(context);
        }
    private void HandleException(ExceptionContext context)
    {
        Type type = context.Exception.GetType();
        if (_exceptionHandlers.ContainsKey(type))
        {
            _exceptionHandlers[type].Invoke(context);
            return;
        }
        if(!context.ModelState.IsValid)
        {
            HandleInvalidModelStateException(context); return;
        }

    }
    private void HandleValidationException(ExceptionContext context)
    {
        var exception = (ValidationException)context.Exception;
        var details = new ValidationProblemDetails(exception.Errors)
        {
            Type = "google.com"
        };
        context.Result= new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }
      
    private void HandleNotFoundException(ExceptionContext context)
    {
        var exception = (NotFoundExxeption)context.Exception;
        var details = new ProblemDetails()
        {
            Type = "",
            Title = "Not found exception"
        };
        context.Result = new NotFoundObjectResult(details);
    }
    private void HandleInvalidModelStateException(ExceptionContext context)
    {
        var details = new ValidationProblemDetails(context.ModelState)
        {
            Type = ""
        };
        context.Result = new BadRequestObjectResult(details);
        context.ExceptionHandled = true;
    }

}

