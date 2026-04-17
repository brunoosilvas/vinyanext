namespace Vinyanext.Application.Abstractions.Databases;

public interface IDbBaseContext : IDisposable
{
    void BeginTransaction();

    Task BeginTransactionAsync(CancellationToken cancellationToken = default!);

    void CommitTransaction();

    Task CommitTransactionAsync(CancellationToken cancellation = default!);
}
