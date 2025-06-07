using Vinyanext.Domain.Dtos.Out.Sistema;
using Vinyanext.Shared.Commons;

namespace Vinyanext.Application.Abstractions.Services.Sistema;

public interface ILoginService : IServiceBase<ILoginService>
{
    Task<Result<LoginOut>> Login(string cpf, string senha, CancellationToken cancellationToken);

}
