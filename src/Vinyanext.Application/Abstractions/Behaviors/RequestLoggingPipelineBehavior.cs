using Vinyanext.Shared.Commons;
using DispatchR.Abstractions.Send;
using Microsoft.Extensions.Logging;
using Serilog.Context;

namespace Vinyanext.Application.Abstractions.Behaviors;

internal sealed class RequestLoggingPipelineBehavior<TRequest, TResponse>(
    ILogger<RequestLoggingPipelineBehavior<TRequest, TResponse>> logger)
    : IPipelineBehavior<TRequest, Task<TResponse>>
    where TRequest : class, IRequest<TRequest, Task<TResponse>>
    where TResponse : Result
{
    public IRequestHandler<TRequest, Task<TResponse>> NextPipeline { get; set; }

    public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
    {
        string requestName = typeof(TRequest).Name;

        logger.LogInformation("Processing request {RequestName}", requestName);

        TResponse result = await NextPipeline.Handle(request, cancellationToken);

        if (result is not null)
        {
            if (result.IsSuccess)
            {
                logger.LogInformation("Completed request {RequestName}", requestName);
            }
            else
            {
                using (LogContext.PushProperty("Error", result.Error, true))
                {
                    logger.LogError("Completed request {RequestName} with error", requestName);
                }
            }
        }
        else
        {
            //result = Result.Failure()
        }

        return result;
    }
}
