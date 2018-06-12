using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace BackendApi
{
    public class AuthOptions
    {
        public const string ISSUER = "MyAuthServer"; // издатель токена
        public const string AUDIENCE = "http://localhost:51884/"; //Token
        const string KEY = "mysupersecret_secretkey!ak13mzxcm123"; //Secret key
        public const int LIFETIME = 60 * 24 * 7; //Token lifetime

        public static SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}