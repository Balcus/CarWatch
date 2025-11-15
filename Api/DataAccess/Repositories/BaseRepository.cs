
using System.ComponentModel.DataAnnotations;
using Api.DataAccess;
using Api.DataAccess.Abstractions;
using Microsoft.EntityFrameworkCore;

public class BaseRepository<TEntity, TId>(DatabaseContext context) : IRepository<TEntity, TId>
    where TEntity : class, IEntityBase<TId>
{
    protected readonly DatabaseContext Context = context;
    

    public virtual async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        try
        {
            return await Context.Set<TEntity>().ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while trying to retrieve all entities: {ex.Message}");
        }
    }

    public virtual async Task<TEntity?> GetByIdAsync(TId id)
    {
        try
        {
            return await Context.Set<TEntity>().FindAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error while trying to retrieve Entity by ID: {ex.Message}");
        }
    }

    public virtual async Task<TId> CreateAsync(TEntity entity)
    {
        try
        {
            Context.Set<TEntity>().Add(entity);
            await Context.SaveChangesAsync();
            return entity.Id;
        }
        catch (DbUpdateException e)
        {
            throw new Exception($"Database error while adding new {typeof(TEntity).Name}: {e.InnerException?.Message ?? e.Message}");
        }
        catch (ValidationException e)
        {
            throw new Exception($"Validation failed while adding {typeof(TEntity).Name}: {e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to add new {typeof(TEntity).Name}: {e.Message}");
        }
    }

    public virtual async Task UpdateAsync(TEntity entity)
    {
        try
        {
            Context.Set<TEntity>().Update(entity);
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            throw new Exception($"Database error while updating {typeof(TEntity).Name}: {e.InnerException?.Message ?? e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to update {typeof(TEntity).Name}: {e.Message}");
        }
    }

    public virtual async Task DeleteAsync(TId id)
    {
        try
        {
            var entity = await Context.Set<TEntity>().FindAsync(id);
            if (entity != null)
            {
                Context.Set<TEntity>().Remove(entity);
                await Context.SaveChangesAsync();
            }
        }
        catch (DbUpdateException e)
        {
            throw new Exception($"Database error while deleting {typeof(TEntity).Name} with ID {id}: {e.InnerException?.Message ?? e.Message}");
        }
        catch (Exception e)
        {
            throw new Exception($"Failed to delete {typeof(TEntity).Name} with ID {id}: {e.Message}");
        }
    }
}