using Api.DataAccess.Abstractions;
using Api.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api.DataAccess.Repositories;

public class UserRepository : BaseRepository<User,int>, IUserRepository
{
    public UserRepository(DatabaseContext context) : base(context)
    {
        
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        return await Context.Users
            .FirstOrDefaultAsync(u => u.Mail == email);
    }
}