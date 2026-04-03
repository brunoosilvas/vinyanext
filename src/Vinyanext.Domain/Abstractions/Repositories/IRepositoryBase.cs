using System.Linq.Expressions;
using Vinyanext.Domain.Abstractions.Databases;

namespace Vinyanext.Domain.Abstractions.Repositories;

public interface IRepositoryBase<TEntity>
    where TEntity : class
{
    void Set(IDbContext context);

    Task<IEnumerable<TEntity>> FindAll(
        IDbContext context,
        bool asTracking = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> Find(
        IDbContext context,
        Expression<Func<TEntity, bool>> where,
        bool asTracking = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> Find(
        IDbContext context,
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, object>>[] includes,
        bool asTracking = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> Find(
        IDbContext context,
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, object>>[] includes,
        int skip,
        int take,
        bool asTracking = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> FindOrderBy(
        IDbContext context,
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, object>>[] includes,
        Expression<Func<TEntity, object>> order,
        bool asTracking = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> FindOrderByAsc(
        IDbContext context,
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, object>>[] includes,
        Expression<Func<TEntity, object>> order,
        int skip,
        int take,
        bool asTracking = false,
        CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> FindOrderByDesc(
        IDbContext context,
        Expression<Func<TEntity, bool>> where,
        Expression<Func<TEntity, object>>[] includes,
        Expression<Func<TEntity, object>> order,
        int skip,
        int take,
        bool asTracking = false,
        CancellationToken cancellationToken = default);

    Task<TEntity> FindById(IDbContext context, int id);

    Task Create(IDbContext context, TEntity obj, bool asDetach = true, CancellationToken cancellationToken = default);

    Task CreateCollection(IDbContext context, IEnumerable<TEntity> entities, bool asDetach = true, CancellationToken cancellationToken = default);

    Task Update(IDbContext context, TEntity obj, bool asDetach = true, CancellationToken cancellationToken = default);

    Task UpdateCollection(IDbContext context, IEnumerable<TEntity> entities, bool asDetach = true, CancellationToken cancellationToken = default);

    Task Delete(IDbContext context, int id, bool asDetach = true, CancellationToken cancellationToken = default);

    Task DeleteWhere(IDbContext context, Func<TEntity, bool> predicate, bool asDetach = true, CancellationToken cancellationToken = default);
}
