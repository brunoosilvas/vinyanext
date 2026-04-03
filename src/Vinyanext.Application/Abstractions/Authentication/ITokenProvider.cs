namespace Vinyanext.Application.Abstractions.Authentication;

public interface ITokenProvider
{
    Task<(string accessToken, string refreshTotken, DateTime? expiraEm)> Create<T>(
        int id,
        string username,
        string password,
        T data)

        where T : class;

    Task<(string username, string password)> GetRefreshToken(string id);
}
