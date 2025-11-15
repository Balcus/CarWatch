using Api.DataAccess.Entities;

namespace Api.DataAccess.Abstractions;

public interface IUserRepository : IRepository<User, int>
{
    Task<User?> GetByEmailAsync(string email);
}