namespace Vinyanext.Domain.Dtos.Out.Sistema;

public sealed record LoginOut(
    string Token,
    string RefreshToken
);
