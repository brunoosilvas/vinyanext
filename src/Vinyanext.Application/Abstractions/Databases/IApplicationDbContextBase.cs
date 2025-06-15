using Microsoft.EntityFrameworkCore;
using Vinyanext.Domain.Entities.Sistema;

namespace Vinyanext.Application.Abstractions.Databases;

public interface IApplicationDbContextBase : IDisposable
{
    DbSet<GsisUsuario> GsisUsuario { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
