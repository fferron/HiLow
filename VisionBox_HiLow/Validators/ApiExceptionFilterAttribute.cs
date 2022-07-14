using HiLow.Entity.Exceptions;
using HiLow.Entity.Exceptions.Models;
using HiLow.Entity.Exceptions.Game.Canceled;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace HiLow.API.Validators
{
    /// <summary>
    /// Request handler to convert exceptions for the corresponding response code and error response message
    /// </summary>
    public class ApiExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> _exceptionHandlers;
        private readonly ILogger<ApiExceptionFilterAttribute> _logger;

        /// <summary>
        /// ctor for exception handler filter with handlers injection
        /// </summary>
        public ApiExceptionFilterAttribute(ILogger<ApiExceptionFilterAttribute> logger)
        {
            // Register known exception types and handlers.
            _exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
                {
                    { typeof(EntityNotFoundException), HandleNotFoundException },
                    { typeof(GameBadRequestException), HandleBadRequestException },
                    { typeof(ForbiddenAccessException), HandleForbiddenAccessException }
                };
        
            _logger = logger;
        }

        /// <summary>
        /// Converter for exceptions
        /// </summary>
        /// <param name="context"></param>
        public override void OnException(ExceptionContext context)
        {
            
            _logger.LogError(context.Exception, $"Failed to process request {context.HttpContext.Request.Path} for the following reason: {context.Exception.Message}");

            HandleException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            
            if (!context.ModelState.IsValid)
            {
                HandleInvalidModelStateException(context);
                return;
            }

            Type type = context.Exception.GetType();
            if (_exceptionHandlers.ContainsKey(type))
            {
                _exceptionHandlers[type].Invoke(context);
                return;
            }

            HandleUnknownException(context);
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            ErrorDetails details = new ErrorDetails()
            {
                StatusCode = 500,
                Message = "Internal Server Error"
            };

            context.Result = new ObjectResult(details)
            {
                StatusCode = StatusCodes.Status500InternalServerError,
            };

            context.ExceptionHandled = true;
        }

        private void HandleInvalidModelStateException(ExceptionContext context)
        {
            ErrorDetails details = new ErrorDetails()
            {
                StatusCode = 400,
                Message = context.ModelState.Values.Where(x => x.ValidationState == Microsoft.AspNetCore.Mvc.ModelBinding.ModelValidationState.Invalid).First().Errors.First().ErrorMessage
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            EntityNotFoundException exception = context.Exception as EntityNotFoundException;

            ErrorDetails details = new ErrorDetails()
            {
                StatusCode = 404,
                Message = exception.Message
            };

            context.Result = new NotFoundObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleBadRequestException(ExceptionContext context)
        {
            GameBadRequestException exception = context.Exception as GameBadRequestException;

            ErrorDetails details = new ErrorDetails()
            {
                StatusCode = 400,
                Message = exception.Message
            };

            context.Result = new BadRequestObjectResult(details);

            context.ExceptionHandled = true;
        }

        private void HandleForbiddenAccessException(ExceptionContext context)
        {
            ForbiddenAccessException exception = context.Exception as ForbiddenAccessException;

            ErrorDetails details = new ErrorDetails()
            {
                StatusCode = 403,
                Message = exception.Message
            };

            context.Result = new ObjectResult(details) 
            { 
                StatusCode = StatusCodes.Status403Forbidden
            };

            context.ExceptionHandled = true;
        }
    }
}
