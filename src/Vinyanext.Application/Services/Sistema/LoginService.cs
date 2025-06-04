using Vinyanext.Application.Abstractions.Database;
using Vinyanext.Application.Abstractions.Services.Sistema;
using Vinyanext.Domain.Dtos.Out.Sistema;
using Vinyanext.Shared.Commons;

namespace Vinyanext.Application.Services.Sistema;

public class LoginService : ILoginService
{
    public IApplicationDbContextBase Context { get; set; } = null!;

    public async Task<Result<LoginOut>> Login(string cpf, string senha)
    {
        var tt = Context;

        return null;
    }

}
