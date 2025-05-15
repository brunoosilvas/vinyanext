using Vinyanext.Application.Abstractions.Messaging;

namespace Vinyanext.Application.UseCases.Sistema.Usuario.Login;

public sealed record LoginCommand(string Usuario, string Senha) : ICommand<string>;
