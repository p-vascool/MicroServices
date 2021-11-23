using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ValidationException = Ordering.Application.Exceptions.ValidationException;

namespace Ordering.Application.Behavior
{
    public class ValidaitonBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidaitonBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);

                var validationsResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(context,cancellationToken)));
                var validationErrors = validationsResults.SelectMany(x => x.Errors).Where(f => f != null).ToList();

                if (validationErrors.Any())
                    throw new ValidationException(validationErrors);
            }

            return await next();
        }
    }
}
