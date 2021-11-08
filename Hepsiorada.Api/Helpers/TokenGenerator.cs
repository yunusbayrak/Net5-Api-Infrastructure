using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hepsiorada.Api.Helpers
{
    public static class TokenGenerator
    {
        public static TokenResponse GenerateToken(string userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("hepsiorada_api_security_key_for_token_validation$hepsiorada");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = "HepsioradaApps",
                Issuer = "hepsiorada.api",
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("UserId", userId)
                }),
                Expires = DateTime.SpecifyKind(DateTime.Now, DateTimeKind.Utc).AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return new TokenResponse()
            {
                Token = tokenHandler.WriteToken(token),
                UserId = userId,
                Expiration = token.ValidTo
            };
        }
    }
    public class TokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string UserId { get; set; }
    }
}
