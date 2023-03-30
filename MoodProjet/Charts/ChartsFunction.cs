using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using MoodProjet.Auth;
using System.Collections.Generic;

namespace MoodProjet.Charts
{
    public static class ChartsFunction
    {
        [FunctionName("Charts-GetChartData")]
        public static IActionResult GetChartData([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            if (JwtHelper.CheckPermissionAndExpirationCorrection(req, JwtHelper.CanSeeCharts))
            {
                List<ChartData> data = ChartsDataManager.GetChartData();
                return new OkObjectResult(data);
            }
            else
            {
                return new BadRequestResult();
            }
        }


        [FunctionName("Charts-GetMoodByHours")]
        public static IActionResult GetMoodByHours([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            if (JwtHelper.CheckPermissionAndExpiration(req, JwtHelper.CanSeeCharts))
            {
                List<MoodByHour> data = ChartsDataManager.GetMoodByHours();
                return new OkObjectResult(data);
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}
