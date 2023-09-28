using MAWService.Application.Common.Helpers.Interfaces;
using System.Web.Http.Filters;

namespace MAWService.Application.Common.Exceptions;

public class HttpResponseExceptionFilter
{
    private readonly bool _isDevelopment;

    public HttpResponseExceptionFilter(bool isDevelopment)
    {
       _isDevelopment = isDevelopment;
    }

    public void OnException(ExceptionContext context)
    {
        if (context.Exception is not CustomException exception)
        {
            if (context.Exception is not FluentValidation.ValidationException validationException)
                return;

            var error = validationException.Errors.FirstOrDefault();
            if (error is null)
                return;

            exception = new ValidationException(error.ErrorMessage);
        }

        var errorResponse = _isDevelopment ?
                new BaseExceptionModel(exception.StatusCode, exception.Message, exception.StackTrace) :
                new BaseExceptionModel(exception.StatusCode, exception.Message);

        context.Result = new ActionResult
        {
            StatusCode = exception.StatusCode < 600 ? exception.StatusCode : 500,
            Value = errorResponse,
        };
        context.ExceptionHandled = true;
    }
}

public class ExceptionContext
{
    public ActionResult Result { get; set; }
    public bool ExceptionHandled { get; set; }
    public Exception Exception{ get; set; }
}

public class ActionResult
{
    public int StatusCode { get; set; }
    public object Value { get; set; }
}