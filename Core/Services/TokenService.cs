using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services
{
    public class TokenService
    {
        private readonly string _secretKey;
        private readonly TimeSpan _expirationTime;

        public TokenService(string secretKey, TimeSpan expirationTime)
        {
            _secretKey = secretKey;
            _expirationTime = expirationTime;
        }

        /// <summary>
        /// Generates a JWT (JSON Web Token) for the specified user ID.
        /// </summary>
        /// <param name="userId">User ID.</param>
        /// <returns>Generated token.</returns>
        public string GenerateToken(long userId)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_secretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, userId.ToString())
                }),
                Expires = DateTime.UtcNow.Add(_expirationTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        /// <summary>
        /// Validates the specified JWT (JSON Web Token).
        /// </summary>
        /// <param name="token">Token to validate.</param>
        /// <returns>True if the token is valid; otherwise, false.</returns>
        public bool ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_secretKey);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
