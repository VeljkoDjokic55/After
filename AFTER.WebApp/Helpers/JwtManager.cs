using AFTER.Shared.DTOs.User.DataOut;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AFTER.WebApp.Helpers
{
    public class JwtManager
    {
        private static readonly string _secret = "Zr4u7x!A%D*G-KaPdRgUkXp2s5v8y/B?E(H+MbQeThVmYq3t6w9z$C&F)J@NcRfUjXnZr4u7x!A%D*G-KaPdSgVkYp3s5v8y/B?E(H+MbQeThWmZq4t7w9z$C&F)J@Nc";
        public static string GetToken(UserDto user, int expireMinutes = 1440)
        {
            // symmetric security key
            var symemtricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    new Claim("email", user.Email),
                    new Claim("name", $"{user.FirstName} {user.LastName}" ),
                }),

                Expires = DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireMinutes)),

                SigningCredentials = new SigningCredentials(symemtricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtHander = new JwtSecurityTokenHandler();
            var token = jwtHander.CreateToken(tokenDescriptor);

            // return token
            return jwtHander.WriteToken(token);
        }

        public static TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                // what to validate
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                // setup validate data
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secret)),
                ClockSkew = TimeSpan.Zero,
                ValidateLifetime = true
            };
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                if (jwtToken == null)
                    return null;

                var symmetricKey = Convert.FromBase64String(_secret);

                var principal = tokenHandler.ValidateToken(token, GetTokenValidationParameters(), out _);

                return principal;
            }

            catch
            {
                return null;
            }
        }
    }
}
