using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ClientService.Security
{
    public class AuthOptions
    {
        public const string ISSUER = "https://localhost:5001"; 
        public const string AUDIENCE = "https://localhost:7118"; 
        const string KEY = "24c4e00522b680d3892f571bbc51f96470708f11c840a3d37718823639ae83c8";   
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
