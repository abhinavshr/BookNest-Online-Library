using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace BookNest.Helpers
{
    internal class CustomJwtSecurityToken
    {
        public string? Issuer { get; private set; }
        public string? Audience { get; private set; }
        public Claim[] Claims { get; private set; }
        public DateTime Expiration { get; private set; }
        public SigningCredentials SigningCredentials { get; private set; }

        public CustomJwtSecurityToken(string? issuer, string? audience, Claim[] claims, DateTime expires, SigningCredentials signingCredentials)
        {
            Issuer = issuer;
            Audience = audience;
            Claims = claims;
            Expiration = expires;
            SigningCredentials = signingCredentials;
        }
    }
}
