using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CodeTur.Shared.Utils
{
    public class Token
    {
        public Token(string issuer, string audience, string secretKey)
        {
            Issuer = issuer;
            Audience = audience;
            SecretKey = secretKey;
        }

        public string Issuer { get; private set; }
        public string Audience { get; private set; }
        public string SecretKey { get; private set; }

        public string GerarJsonWebToken(Guid id, string nome, string email, string tipoUsuario)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecretKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                new Claim(JwtRegisteredClaimNames.FamilyName, nome),
                new Claim(JwtRegisteredClaimNames.Email, email),
                new Claim(ClaimTypes.Role,tipoUsuario),
                new Claim("role",tipoUsuario),
                new Claim(JwtRegisteredClaimNames.Jti, id.ToString())
            };

            var token = new JwtSecurityToken
                (
                    Issuer,
                    Audience,
                    claims,
                    expires: DateTime.Now.AddMinutes(120),
                    signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
