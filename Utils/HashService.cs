using System.Security.Cryptography;
using System.Text;

namespace TodoApp.Utils;

public class HashService
{
    public static string ComputeSHA256(string raw)
    {
        var sha256Hash = SHA256.Create();
        
        var bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(raw));

        var builder = new StringBuilder();

        foreach (var b in bytes) 
        { 
            builder.Append(b.ToString("x2"));
        }
        
        return builder.ToString();
    }
}