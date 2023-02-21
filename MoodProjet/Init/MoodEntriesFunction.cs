using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace MoodProjet.Init
{
    public static class InitFunction
    {
        [FunctionName("Init-CheckDB")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "CheckDB")] HttpRequest req)
        {
            return new OkObjectResult(InitDataManager.CheckDbConn());
        }

        [FunctionName("Init-InitDB")]
        public static async Task<IActionResult> InitDB([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "InitDB")] HttpRequest req)
        {
            return new OkObjectResult(InitDataManager.InitDB());
        }


        [FunctionName("Init-SetConfigurationTables")]
        public static async Task<IActionResult> SetConfigurationTables([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "SetConfigurationTables")] HttpRequest req)
        {
            return new OkObjectResult(InitDataManager.SetConfigurationTables());
        }




        [FunctionName("Init-SetEntriesTables")]
        public static async Task<IActionResult> UpdateMoodEntry([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "SetEntriesTables")] HttpRequest req, ILogger log)
        {
            return new OkObjectResult(InitDataManager.SetEntriesTables());
        }





        [FunctionName("Init-KickStart")]
        public static async Task<IActionResult> KickStart([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "KickStart")] HttpRequest req, ILogger log)
        {
            var res = new
            {
                CheckDbConn = InitDataManager.CheckDbConn(),
                InitDB = InitDataManager.CheckDbConn(),
                SetConfigurationTables = InitDataManager.SetConfigurationTables(),
                SetEntriesTables = InitDataManager.SetEntriesTables(),
            };

            return new OkObjectResult(res);
        }

    }
}
