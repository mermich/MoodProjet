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
                Expires = DateTime.UtcNow.AddDays(7),
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

        public static string GetClaim(string token, string claimType)
        {
            token = token.Replace("Bearer ", "");

            JwtSecurityTokenHandler tokenHandler = new();
            JwtSecurityToken securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            string stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }

        public static bool GetClaimAsBool(HttpRequest request, string claimType)
        {
            if (request.Headers.ContainsKey("Authorization"))
            {
                string claimString = GetClaim(request.Headers["Authorization"], claimType);
                return claimString.ToLowerInvariant() == true.ToString().ToLowerInvariant();
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