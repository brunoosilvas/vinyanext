using Vinyanext.Domain.Entities.Sistema;

namespace Vinyanext.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    string Create(GsisUsuario usuario);
}
