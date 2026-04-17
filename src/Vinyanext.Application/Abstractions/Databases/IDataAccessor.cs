using Vinyanext.Domain.Abstractions.Databases;

namespace Vinyanext.Application.Abstractions.Databases;

public interface IDataAccessor
{
    public IDbContext DbContext { get; }
    public IMdbContext MdbContext { get; }
}
