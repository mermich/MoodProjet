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

namespace MoodProjet.Auth
{
    public static class MoodFaceFunction
    {

        private const string PayloadKey = "key";
        [FunctionName("Auth-Login")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Login")] HttpRequest req)
        {

            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            UserLogin userLogin = JsonConvert.DeserializeObject<UserLogin>(requestBody);

            var loginResult = AuthDataManager.Login(userLogin);

            var res = new
            {
                Login = loginResult.Login,
                Token = JwtHelper.GenerateToken(loginResult),
                IsLoginOK = loginResult.IsLoginOK
            };

            return new OkObjectResult(res);
        }
    }
}