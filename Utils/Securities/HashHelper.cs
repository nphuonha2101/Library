using System.Security.Cryptography;
using System.Text;

namespace Library.Utils.Securities;

public class HashHelper
{
    public static string secretKey = "MotHayBa";
    public static string Hash(string password, string key)
    {
        var passwordBytes = Encoding.UTF8.GetBytes(password);
        var secretKeyBytes = Encoding.UTF8.GetBytes(key);
        
        using var hmac = new HMACSHA256(secretKeyBytes);
        var hash = hmac.ComputeHash(passwordBytes);
        
        return BitConverter.ToString(hash).Replace("-", "");
    }
}