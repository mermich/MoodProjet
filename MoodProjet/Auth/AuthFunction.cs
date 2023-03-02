using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Newtonsoft.Json;
using System.IO;
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

            UserLoginResult loginResult = AuthDataManager.Login(userLogin);

            var res = new
            {
                loginResult.Login,
                Token = JwtHelper.GenerateToken(loginResult),
                loginResult.IsLoginOK
            };

            return new OkObjectResult(res);
        }
    }
}