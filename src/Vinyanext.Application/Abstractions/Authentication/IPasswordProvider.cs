namespace Vinyanext.Application.Abstractions.Authentication;

public interface IPasswordProvider
{
    string Hash(string password);

    bool Verify(string password, string passwordHash);
}
