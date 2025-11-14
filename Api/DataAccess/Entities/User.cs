using Api.DataAccess.Enums;

namespace Api.DataAccess.Entities;

public class User
{
    public string Name { get; set; }
    public string Mail { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }
}