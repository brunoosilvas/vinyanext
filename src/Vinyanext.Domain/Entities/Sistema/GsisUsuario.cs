using Vinyanext.Shared.Commons;

namespace Vinyanext.Domain.Entities.Sistema;

public sealed class GsisUsuario : Entity
{
    public int Id { get; set; }
    public string? Cpf { get; set; }
    public string? Senha { get; set; }
    
}
