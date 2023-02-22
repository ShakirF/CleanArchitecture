using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Application.Common.Behaviours
{
    public class UnhandledExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
		ILogger<TRequest> _logger;

        public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
        {
            this._logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
			try
			{
				return await next();

			}
			catch (Exception ex)
			{

				var requestName = typeof(TRequest).Name;
                _logger.LogError(ex, "CleanArchitecture Request : Unhandled Exception for Request (Name) (@requestName)", requestName, request);
                throw;
			}
        }
    }

    public class ValidationExceptionBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationExceptionBehaviour(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken)));
                var failures = validationResults.Where(r=>r.Errors.Any())
                    .SelectMany(r=> r.Errors)
                    .ToList();

                if (failures.Any())
                {
                    throw new CleanArchitecture.Application.Common.Exceptions.ValidationException(failures);
                }
            }
            return await next();
        }
    }
}
