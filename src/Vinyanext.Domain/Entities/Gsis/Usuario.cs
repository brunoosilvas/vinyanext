using Vinyanext.Shared.Commons;

namespace Vinyanext.Domain.Entities.Gsis;

public sealed class Usuario : Entity
{
    public int Id { get; set; }
    public string? Cpf { get; set; }
    public string? Senha { get; set; }

}
