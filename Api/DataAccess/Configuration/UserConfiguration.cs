using Api.DataAccess.Entities;
using Api.DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Api.DataAccess.Configuration;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        
        builder.HasData(
            new User
            {
                Id = 1,
                Name = "John Doe",
                Mail = "john.doe@example.com",
                Password = "hashed_password_123",
                Role = Role.Default
            },
            new User
            {
                Id = 2,
                Name = "Jane Smith",
                Mail = "jane.smith@example.com",
                Password = "hashed_password_456",
                Role = Role.Default
            },
            new User
            {
                Id = 4,
                Name = "Mike Johnson",
                Mail = "mike.johnson@example.com",
                Password = "hashed_password_101",
                Role = Role.Default
            }
        );
    }
}