using Vinyanext.Application.Abstractions.Messaging;

namespace Vinyanext.Application.UseCases.Sistema.Usuario.Login;

public sealed record LoginCommand(string Cpf, string Senha) : ICommand<string>;
