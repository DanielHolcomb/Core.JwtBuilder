using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Core.JwtBuilder
{
    public class JwtTokenGenerator
    {
        /// <summary>
        /// Generate a basic JWT token with an expiration of 1 hour.
        /// </summary>
        /// <param name="config"></param>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static string GenerateToken(IConfiguration config, IEnumerable<Claim> claims)
        {
            return GenerateToken(config, claims, null);
        }

        /// <summary>
        /// Generate a basic JWT token. Expiration defaults to 1 hour
        /// </summary>
        /// <param name="config"></param>
        /// <param name="claims"></param>
        /// <param name="expiration"></param>
        /// <returns></returns>
        public static string GenerateToken(IConfiguration config, IEnumerable<Claim> claims, DateTime? expiration)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(config.GetSection("Jwt:Key").Value);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = expiration ?? DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = config.GetSection("Jwt:Issuer").Value,
                Audience = config.GetSection("Jwt:Audience").Value,
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
