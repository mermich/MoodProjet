using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace MoodProjet.Auth
{
    public static class JwtHelper
    {
        private const string secret = "asdv234234^&%&^%&^hjsdfb2%%%";
        public static string GenerateToken(UserLoginResult loginResult)
        {
            SymmetricSecurityKey mySecurityKey = new(Encoding.ASCII.GetBytes(secret));

            string myIssuer = "http://localhost";
            string myAudience = "http://localhost";

            JwtSecurityTokenHandler tokenHandler = new();
            SecurityTokenDescriptor tokenDescriptor = new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, loginResult.Login.ToString()),
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature),
                Claims = new Dictionary<string, object> {
                    { CanAdminDevices, loginResult.CanAdminDevices },
                    { CanAdminMoodEntries, loginResult.CanAdminMoodEntries },
                    { CanAdminMoodFaces, loginResult.CanAdminMoodFaces },
                    { CanSeeCharts, loginResult.CanSeeCharts }
                }
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public static bool ValidateCurrentToken(string token)
        {
            SymmetricSecurityKey mySecurityKey = new(Encoding.ASCII.GetBytes(secret));

            string myIssuer = "http://localhost";
            string myAudience = "http://localhost";

            JwtSecurityTokenHandler tokenHandler = new();
            try
            {
                _ = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch
            {
                return false;
            }
            return true;
        }



        private static JwtSecurityToken ExtractJwtSecurityTokenFromRequest(HttpRequest request)
        {
            if (request.Headers.ContainsKey("Authorization"))
            {
                Microsoft.Extensions.Primitives.StringValues a = request.Headers["Authorization"];

                string token = a.ToString().Replace("Bearer ", "");
                JwtSecurityTokenHandler tokenHandler = new();
                JwtSecurityToken jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;
                return jwtToken;
            }
            else
            {
                return null;
            }
        }



        public static bool HasClaim(JwtSecurityToken jwtToken, string claimType)
        {
            if (jwtToken == null || jwtToken.Claims == null || jwtToken.Claims.Count() == 0)
            {
                return false;
            }
            else
            {
                Claim matchingClaim = jwtToken.Claims.FirstOrDefault(claim => claim.Type == claimType);
                if (matchingClaim != null)
                {
                    string stringClaimValue = matchingClaim.Value;
                    return stringClaimValue.ToLowerInvariant() == true.ToString().ToLowerInvariant();
                }
                else
                {
                    return false;
                }
            }
        }

        public static bool CheckPermissionAndExpiration(HttpRequest request, string claimType)
        {
            JwtSecurityToken jwtToken = ExtractJwtSecurityTokenFromRequest(request);

            if (HasClaim(jwtToken, claimType))
            {
                return jwtToken.ValidTo >= DateTime.UtcNow;
            }
            else
            {
                return false;
            }
        }

        public const string CanAdminDevices = "CanAdminDevices";
        public const string CanAdminMoodEntries = "CanAdminMoodEntries";
        public const string CanAdminMoodFaces = "CanAdminMoodFaces";
        public const string CanSeeCharts = "CanSeeCharts";
    }
}