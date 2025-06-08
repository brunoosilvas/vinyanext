using FluentValidation;
using Vinyanext.Shared.Constants;

namespace Vinyanext.Application.UseCases.Sistema.Usuario.Login;

internal sealed class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(c => c.Login.Cpf).NotEmpty();
        RuleFor(c => c.Login.Senha).NotEmpty();
        RuleFor(c => c.Login.Tipo)
            .NotEmpty()
            .Must(v => new[] { Authentications.Token, Authentications.RefreshToken }.Contains(v));
    }

}
