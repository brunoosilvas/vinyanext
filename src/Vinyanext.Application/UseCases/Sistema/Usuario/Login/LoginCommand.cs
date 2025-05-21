using Vinyanext.Application.Abstractions.Messaging;
using Vinyanext.Domain.Dtos.In.Sistema;
using Vinyanext.Domain.Dtos.Out.Sistema;

namespace Vinyanext.Application.UseCases.Sistema.Usuario.Login;

public sealed record LoginCommand(LoginIn Login) : ICommand<LoginOut>;
