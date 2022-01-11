using FluentValidation;
using MediatR;
using ShopsRUs.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ShopsRUs.Domain.Pipelines
{
     public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> validators;

        public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators) {
            this.validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next) {
            if (validators == null || !validators.Any()) {
                return await next();
            }

            var validationResult = validators.Select(v => v.Validate(request)).SelectMany(result => result.Errors).ToList();
            if (!validationResult.Any()) {
                return await next();
            }

            throw new CustomException(string.Join(",", validationResult.Select(e => e.ErrorMessage)));
        }
    }
}
