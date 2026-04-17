using Vinyanext.Shared.Commons;
using DispatchR.Abstractions.Send;
using FluentValidation;
using FluentValidation.Results;
using System.Reflection;

namespace Vinyanext.Application.Abstractions.Behaviors;

internal sealed class ValidationPipelineBehavior<TRequest, TResponse>(
    IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, Task<TResponse>>
    where TRequest : class, IRequest<TRequest, Task<TResponse>>
{
    public IRequestHandler<TRequest, Task<TResponse>> NextPipeline { get; set; }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        var validationFailures = await ValidateAsync(request);

        if (validationFailures.Length == 0)
        {
            return await NextPipeline.Handle(request, cancellationToken);
        }

        if (typeof(TResponse).IsGenericType &&
            typeof(TResponse).GetGenericTypeDefinition() == typeof(Result<>))
        {
            var resultType = typeof(TResponse).GetGenericArguments()[0];

            var failureMethod = typeof(Result<>)
                .MakeGenericType(resultType)
                .GetMethod(nameof(Result<object>.ValidationFailure));

            if (failureMethod is not null)
            {
                return (TResponse)failureMethod.Invoke(
                    null,
                    [CreateValidationError(validationFailures)]);
            }
        }
        else if (typeof(TResponse) == typeof(Result))
        {
            return (TResponse)(object)Result.Failure(CreateValidationError(validationFailures));
        }

        throw new ValidationException(validationFailures);
    }

    private async Task<ValidationFailure[]> ValidateAsync(TRequest request)
    {
        if (!validators.Any())
        {
            return [];
        }

        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            validators.Select(validator => validator.ValidateAsync(context)));

        ValidationFailure[] validationFailures = [.. validationResults
                .Where(validationResult => !validationResult.IsValid)
                .SelectMany(validationResult => validationResult.Errors)];

        return validationFailures;
    }

    private static ValidationError CreateValidationError(ValidationFailure[] validationFailures) =>
        new([.. validationFailures.Select(f => Error.Problem(f.ErrorCode, f.ErrorMessage))]);
}
