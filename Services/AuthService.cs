using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using BookNest.Helpers;

namespace BookNest.Services
{
    public class AuthService
    {
        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GenerateJwtToken(string userId, string username)
        {
            // Set token expiration (e.g., 1 hour)
            var expirationTime = DateTime.UtcNow.AddHours(1);

            // Define claims
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, userId),
                new Claim(ClaimTypes.Name, username),
                new Claim("role", "Admin")  // Add the role claim if needed
            };

            // Signing credentials
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Create the CustomJwtSecurityToken
            var customToken = new CustomJwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims.ToArray(),
                expires: expirationTime,
                signingCredentials: signingCredentials
            );

            // Create JWT token using JwtSecurityTokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = new JwtSecurityToken(
                issuer: customToken.Issuer,
                audience: customToken.Audience,
                claims: customToken.Claims,
                expires: customToken.Expiration,
                signingCredentials: customToken.SigningCredentials
            );

            // Return token as a string
            return tokenHandler.WriteToken(securityToken);
        }
    }
}
