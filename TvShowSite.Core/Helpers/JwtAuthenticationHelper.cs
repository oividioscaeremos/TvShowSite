using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TvShowSite.Core.Helpers
{
    public class JwtAuthenticationHelper
    {
        public static string PublicKey
        {
            get
            {
                var publicKey = string.Empty;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Keys", "public_key.pem");
                
                using (var reader = File.OpenText(path))
                {
                    publicKey = reader.ReadToEnd();
                }

                return publicKey;
            }
        }

        public static string PrivateKey
        {
            get
            {
                var privateKey = string.Empty;

                var path = Path.Combine(Directory.GetCurrentDirectory(), "Keys", "private_key.pem");

                using (var reader = File.OpenText(path))
                {
                    privateKey = reader.ReadToEnd();
                }

                return privateKey;
            }
        }

        public static string CreateToken(Dictionary<string, object> claims, DateTime expiresAt)
        {
            var rsa = RSA.Create();

            rsa.ImportFromPem(PrivateKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Claims = claims,
                Expires = expiresAt,
                SigningCredentials = new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256),
                Issuer = "TvShowSiteApiBackend",
                Audience = "TvShowSitePortalFrontEnd"
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            SecurityToken securityToken = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(securityToken); ;
        }

        public static string CreateRefreshToken()
        {
            var randomNumber = new byte[32];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);

                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
