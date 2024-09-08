using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Newsletter.Shared.Application.Behaviours.Validation;

internal sealed class ValidationPipelineBehaviour<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators
) : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        ValidationContext<TRequest> context = new(request);

        ValidationResult[] validationResults = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context, cancellationToken))
        );

        List<ValidationFailure> errors = validationResults
            .Where(validationResult => !validationResult.IsValid)
            .SelectMany(validationResult => validationResult.Errors)
            .Select(validationFailure => new ValidationFailure
            (
                validationFailure.PropertyName,
                validationFailure.ErrorMessage
            ))
            .ToList();

        if (errors.Count is not 0)
            throw new ValidationException(errors);

        TResponse response = await next();

        return response;
    }
}