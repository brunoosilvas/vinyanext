using MediatR;
using Vinyanext.Shared.Commons;

namespace Vinyanext.Application.Abstractions.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
