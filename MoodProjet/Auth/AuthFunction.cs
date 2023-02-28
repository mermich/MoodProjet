using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MoodProjet.Auth {
    public static class MoodFaceFunction {

        private const string PayloadKey = "key";
        [FunctionName("Auth-Login")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Login")] HttpRequest req) {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            UserLogin userLogin = JsonConvert.DeserializeObject<UserLogin>(requestBody);

            var loginResult = AuthDataManager.Login(userLogin);

            var res = new {
                Login = loginResult.Login,
                Token = GenerateToken(loginResult),
                IsLoginOK = loginResult.IsLoginOK
            };

            return new OkObjectResult(res);
        }

        public static string GenerateToken(UserLoginResult loginResult) {
            var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = "http://localhost";
            var myAudience = "http://localhost";

            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, loginResult.Login.ToString()),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                Issuer = myIssuer,
                Audience = myAudience,
                SigningCredentials = new SigningCredentials(mySecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            tokenDescriptor.Claims = new Dictionary<string, object> {
                { "CanAdminDevices", loginResult.CanAdminDevices },
                { "CanAdminMoodEntries", loginResult.CanAdminMoodEntries },
                { "CanAdminMoodFaces", loginResult.CanAdminMoodFaces },
                { "CanSeeCharts", loginResult.CanSeeCharts }
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


        public static bool ValidateCurrentToken(string token) {
            var mySecret = "asdv234234^&%&^%&^hjsdfb2%%%";
            var mySecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(mySecret));

            var myIssuer = "http://localhost";
            var myAudience = "http://localhost";

            var tokenHandler = new JwtSecurityTokenHandler();
            try {
                tokenHandler.ValidateToken(token, new TokenValidationParameters {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = myIssuer,
                    ValidAudience = myAudience,
                    IssuerSigningKey = mySecurityKey
                }, out SecurityToken validatedToken);
            }
            catch {
                return false;
            }
            return true;
        }

        public static string GetClaim(string token, string claimType) {
            token = token.Replace("Bearer ", "");

            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }
    }
}