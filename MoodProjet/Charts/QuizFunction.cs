using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MoodProjet.Charts
{
    public static class QuizFunction
    {
        //[FunctionName("GetSampleDataQueries")]
        //public static async Task<IActionResult> GetSampleDataQueries([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        //{
        //    string query = "";
        //    List<DateTime> days = new List<DateTime>
        //    {
        //        new DateTime(2023, 1, 23, 7, 30, 0),
        //        new DateTime(2023, 1, 24, 7, 30, 0),
        //        new DateTime(2023, 1, 25, 7, 30, 0),
        //        new DateTime(2023, 1, 26, 7, 30, 0),
        //        new DateTime(2023, 1, 27, 7, 30, 0),

        //        new DateTime(2023, 1, 30, 7, 30, 0),
        //        new DateTime(2023, 1, 31, 7, 30, 0),
        //        new DateTime(2023, 2, 1, 7, 30, 0),
        //        new DateTime(2023, 2, 2, 7, 30, 0),
        //        new DateTime(2023, 2, 3, 7, 30, 0),

        //        new DateTime(2023, 2, 6, 7, 30, 0),
        //        new DateTime(2023, 2, 7, 7, 30, 0),
        //        new DateTime(2023, 2, 8, 7, 30, 0),
        //        new DateTime(2023, 2, 9, 7, 30, 0),
        //        new DateTime(2023, 2, 10, 7, 30, 0),

        //        new DateTime(2023, 2, 13, 7, 30, 0),
        //        new DateTime(2023, 2, 14, 7, 30, 0)
        //    };

        //    for (int i = 0; i < 10000; i++)
        //    {
        //        DateTime randomday = days[new Random().Next(0, days.Count - 1)];
        //        DateTime time = randomday.AddMinutes(new Random().Next(0, 500));

        //        int randomVal = new Random().Next(1, 5);

        //        query += $"INSERT INTO MoodEntry (`Date`,MoodDeviceId,MoodFaceId) VALUES ('{time.ToString("yyyy-MM-dd H:mm:ss")}','1','{randomVal}');" + Environment.NewLine;
        //    }


        //    return new OkObjectResult(query);
        //}


        [FunctionName("Charts-GetChartData")]
        public static async Task<IActionResult> GetChartData([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            List<ChartData> data = ChartsDataManager.GetChartData();
            return new OkObjectResult(data);
        }
    }
}
