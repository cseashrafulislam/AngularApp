using System.Linq.Expressions;


public interface IRepository<TEntity> where TEntity : class
{
    TEntity Find(int id);
    Task<TEntity> FindAsync(int id);

    IEnumerable<TEntity> GetAll();
    Task<IEnumerable<TEntity>> GetAllAsync();

    IQueryable<TEntity> GetAllQueryable();

    IEnumerable<TEntity> Where(Expression<Func<TEntity, bool>> predicate);
    Task<IEnumerable<TEntity>> WhereAsync(Expression<Func<TEntity, bool>> predicate);

    TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate);
    Task<TEntity> SingleOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);

    bool Any(Expression<Func<TEntity, bool>> predicate);
    Task<bool> AnyAsync(Expression<Func<TEntity, bool>> predicate);

    void Add(TEntity entity);
    Task AddAsync(TEntity entity);

    void AddRange(IEnumerable<TEntity> entities);
    Task AddRangeAsync(IEnumerable<TEntity> entities);

    void Remove(TEntity entity);
    void RemoveRange(IEnumerable<TEntity> entities);

    void Update(TEntity entity);
    void UpdateRange(IEnumerable<TEntity> entities);

    Task<(IEnumerable<TEntity> Items, int TotalCount)> GetPaginatedAsync(
        int pageNumber, int pageSize, Expression<Func<TEntity, bool>> filter = null);
}

