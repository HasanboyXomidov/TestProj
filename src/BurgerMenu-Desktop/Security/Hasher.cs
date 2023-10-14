using System;

namespace BurgerMenu_Desktop.Security;

public class Hasher
{
    public static (string PasswordHash,string Salt) Hash(string Password)
    {
        string salt = GenerateSalt();
        var hash = BCrypt.Net.BCrypt.HashPassword(Password + salt);
        return (hash, salt);
    }
    public static string GenerateSalt() => Guid.NewGuid().ToString();
    public static bool Verify(string Password,string PasswordHash,string Salt)
    {
        return BCrypt.Net.BCrypt.Verify(Password+Salt, PasswordHash);
    }
}
