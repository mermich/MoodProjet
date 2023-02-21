using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoodProjet.Charts
{
    public static class ChartsFunction
    {
        [FunctionName("Charts-GetChartData")]
        public static async Task<IActionResult> GetChartData([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            List<ChartData> data = ChartsDataManager.GetChartData();
            return new OkObjectResult(data);
        }


        [FunctionName("Charts-GetMoodByHours")]
        public static async Task<IActionResult> GetMoodByHours([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            List<MoodByHour> data = ChartsDataManager.GetMoodByHours();
            return new OkObjectResult(data);
        }
    }
}
