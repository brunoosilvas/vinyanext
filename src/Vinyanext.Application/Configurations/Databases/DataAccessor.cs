using Vinyanext.Application.Abstractions.Databases;
using Vinyanext.Domain.Abstractions.Databases;

namespace Vinyanext.Application.Configurations.Databases;

public class DataAccessor(
    IDbContext dbContext = null,
    IMdbContext mdbContext = null)
    : IDataAccessor
{
    public IDbContext DbContext { get; } = dbContext;

    public IMdbContext MdbContext { get; } = mdbContext;
}
