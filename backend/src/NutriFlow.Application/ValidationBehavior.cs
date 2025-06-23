using FluentResults;
using FluentValidation;
using MediatR;

namespace NutriFlow.Application
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var context = new ValidationContext<TRequest>(request);
                var validationResults = await Task.WhenAll(
                    _validators.Select(v => v.ValidateAsync(context, cancellationToken))
                );

                var failures = validationResults
                    .SelectMany(r => r.Errors)
                    .Where(f => f != null)
                    .ToList();

                if (failures.Count != 0)
                {
                    if (typeof(TResponse) == typeof(Result))
                    {
                        var result = new Result();
                        foreach (var failure in failures)
                            result.WithError(failure.ErrorMessage);

                        return (TResponse)(object)result;

                    }

                    throw new ValidationException(failures);
                }
            }

            return await next();
        }

    }
}
