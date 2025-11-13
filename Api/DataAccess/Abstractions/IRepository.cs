namespace Api.DataAccess.Abstractions;

public interface IRepository<TEntity, in TId> where TEntity : IEntityBase<TId>
{
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity?> GetByIdAsync(TId id);
    Task CreateAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TId id);
}