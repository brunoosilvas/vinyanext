using Vinyanext.Domain.Abstractions.Databases;

namespace Vinyanext.Domain.Abstractions.Repositories;

public interface IRepositoryNoSqlBase<TEntity>
{
    string CollectionName { get; set; }

    void Set(IMdbContext context);

    Task<TEntity> FindById(IMdbContext context, string id);

    Task Create(IMdbContext context, TEntity value);

    Task Update(IMdbContext context, string id, TEntity value);

    Task Delete(IMdbContext context, string id);
}
