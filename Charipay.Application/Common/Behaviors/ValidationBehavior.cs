using Charipay.Application.Common.Models;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charipay.Application.Common.Behaviors
{
    /// <summary>
    /// MediatR pipeline behavior that runs all FluentValidation validators
    /// for a given request before it passing to the handler
    /// </summary>
    /// <typeparam name="TRequest">The request type (command or query).</typeparam>
    /// <typeparam name="TResponse">The response type (must be ApiResponse&lt;T&gt;).</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest:notnull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ValidationBehavior{TRequest, TResponse}"/> class.
        /// </summary>
        /// <param name="validators">
        /// A collection of FluentValidation validators that apply to the <typeparamref name="TRequest"/>.
        /// </param>
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if(_validators.Any())
            {
                // run all validators against the request
                // 
                var context = new ValidationContext<TRequest>(request);

                var failures = _validators
                    .Select(c=>c.Validate(context))
                    .SelectMany(c=>c.Errors)
                    .Where(c=>c is not null)
                    .ToList();
               
                //If there are any validation failures, return an ApiResponse<T> with errors
                if(failures.Any())
                {
                    var message = failures
                        .Select(c=>c.ErrorMessage).ToList();

                    return CreateFailResponse(message);
                }

            }

            // continue to the handler if no validation issue
            return await next();
        }

        ///<summary>
        /// Builds an <see cref="ApiResponse{T}"/> failure result dynamically
        /// for the expected <typeparamref name="TResponse"/>.
        /// </summary>
        /// <param name="errors">The list of validation error messages.</param>
        private static TResponse CreateFailResponse(List<string> errors)
        {
            var tRes = typeof(TResponse);

            if(tRes.IsGenericType || tRes.GetGenericTypeDefinition() != typeof(ApiResponse<>))
            {
                throw new InvalidOperationException("InvalidBehavior expects all responses to be ApiResponse<T>.");
            }

            var payloadType = tRes.GetGenericArguments()[0];
            var apiResponseType = typeof(ApiResponse<>).MakeGenericType(payloadType);

            var failMethod = apiResponseType.GetMethod("FailResponse", new[] { typeof(string), typeof(List<string>) });
            return (TResponse)failMethod.Invoke(null, new object[] { "validation failed", errors })!; 
        }
    }
}
