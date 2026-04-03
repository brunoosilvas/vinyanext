namespace Vinyanext.Domain.Dtos.Out.Gsis;

public sealed record LoginOut(
    string Token,
    string RefreshToken
);
