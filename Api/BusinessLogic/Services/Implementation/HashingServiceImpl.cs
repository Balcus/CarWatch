using Microsoft.AspNetCore.Identity;

namespace Api.BusinessLogic.Services.Implementation;

public class HashingServiceImpl
{
    public string HashPassword(string password)
    {
        return BCrypt.Net.BCrypt.EnhancedHashPassword(password);
    }

    public bool VerifyPassword(string hashedPassword, string password)
    {
        return BCrypt.Net.BCrypt.EnhancedVerify(password, hashedPassword);
    }
}