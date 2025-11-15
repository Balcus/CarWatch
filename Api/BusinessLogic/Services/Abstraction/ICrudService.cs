namespace Api.BusinessLogic.Services.Abstraction;

public interface ICrudService<T, TId> where T : class
{
    public Task<List<T>> GetAllAsync();
    public Task<T> GetByIdAsync(TId id);
    public Task<TId> CreateAsync(T entity);
    public Task<TId> UpdateAsync(TId id, T entity);
    public Task<TId> DeleteAsync(T entity);
}