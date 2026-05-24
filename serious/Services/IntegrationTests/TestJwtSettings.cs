using System;
using System.Security.Cryptography;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace IntegrationTests
{
    public static class TestJwtSettings
    {
        private static readonly RSA _rsa;
        public static readonly string ValidRsaPublicKey;

        static TestJwtSettings()
        {
            _rsa = RSA.Create(2048);
            ValidRsaPublicKey = Convert.ToBase64String(_rsa.ExportSubjectPublicKeyInfo());
        }

        public static void ConfigureTestEnvironment()
        {
            Environment.SetEnvironmentVariable("JwtSettings__RsaPublicKey", ValidRsaPublicKey);
            Environment.SetEnvironmentVariable("UseInMemoryDatabase", "true");
            Environment.SetEnvironmentVariable("ConnectionStrings__DefaultConnection", "Server=dummy;Database=dummy;User Id=dummy;Password=dummy;TrustServerCertificate=True;");
            Environment.SetEnvironmentVariable("ApiSettings__CatalogUrl", "http://localhost");
            Environment.SetEnvironmentVariable("ApiSettings__IdentityUrl", "http://localhost");
        }

        public static string GenerateJwtToken(string username = "tester", string role = "user")
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = new RsaSecurityKey(_rsa);
            
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim(ClaimTypes.Role, role),
                    new Claim("role", role)
                }),
                Expires = DateTime.UtcNow.AddHours(2),
                Issuer = "identity-api",
                Audience = "identity-client",
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
