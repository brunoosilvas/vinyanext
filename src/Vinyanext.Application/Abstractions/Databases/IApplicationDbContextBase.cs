using Microsoft.EntityFrameworkCore;
using Vinyanext.Domain.Entities.Gsis;

namespace Vinyanext.Application.Abstractions.Databases;

public interface IApplicationDbContextBase : IDisposable
{
    DbSet<Usuario> Usuario { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
