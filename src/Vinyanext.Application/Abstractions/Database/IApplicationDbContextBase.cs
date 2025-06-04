using Microsoft.EntityFrameworkCore;
using Vinyanext.Domain.Entities.Sistema;

namespace Vinyanext.Application.Abstractions.Database;

public interface IApplicationDbContextBase
{
    DbSet<GsisUsuario> GsisUsuario { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
