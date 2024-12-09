using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace WebAppServer
{
    public class AuthOptions
    {
        public const string ISSUER = "Web_App_Server";
        public const string AUDIENCE = "WebAPI_App_Client";
        public const string KEY = "80C81D31-BDA5-4030-B3A1-D12AFDC553E4";
        public const int LIFETIME = 1;

        public static SymmetricSecurityKey GetSynmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(KEY));
        }
    }
}
