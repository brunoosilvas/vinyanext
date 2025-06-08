using Vinyanext.Domain.Entities.Sistema;
using Vinyanext.Shared.Commons;

namespace Vinyanext.Application.Abstractions.Services.Sistema;

public interface ILoginService : IServiceBase<ILoginService>
{
    Task<Result<GsisUsuario>> Login(string cpf, string senha, CancellationToken cancellationToken);

}
