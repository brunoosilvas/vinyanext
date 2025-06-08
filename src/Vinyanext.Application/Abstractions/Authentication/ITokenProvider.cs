using Vinyanext.Domain.Entities.Sistema;

namespace Vinyanext.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    Task<(string token, string refreshTotken)> Create(GsisUsuario usuario);
}
