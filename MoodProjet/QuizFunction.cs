using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using MoodProjet.db;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MoodProjet
{
    public static class QuizFunction
    {

        [FunctionName("GetSampleDataQueries")]
        public static async Task<IActionResult> GetSampleDataQueries([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            var query = "";
            var days = new List<DateTime>();

            days.Add(new DateTime(2023, 1, 23, 7, 30, 0));
            days.Add(new DateTime(2023, 1, 24, 7, 30, 0));
            days.Add(new DateTime(2023, 1, 25, 7, 30, 0));
            days.Add(new DateTime(2023, 1, 26, 7, 30, 0));
            days.Add(new DateTime(2023, 1, 27, 7, 30, 0));

            days.Add(new DateTime(2023, 1, 30, 7, 30, 0));
            days.Add(new DateTime(2023, 1, 31, 7, 30, 0));
            days.Add(new DateTime(2023, 2, 1, 7, 30, 0));
            days.Add(new DateTime(2023, 2, 2, 7, 30, 0));
            days.Add(new DateTime(2023, 2, 3, 7, 30, 0));

            days.Add(new DateTime(2023, 2, 6, 7, 30, 0));
            days.Add(new DateTime(2023, 2, 7, 7, 30, 0));
            days.Add(new DateTime(2023, 2, 8, 7, 30, 0));
            days.Add(new DateTime(2023, 2, 9, 7, 30, 0));
            days.Add(new DateTime(2023, 2, 10, 7, 30, 0));

            days.Add(new DateTime(2023, 2, 13, 7, 30, 0));
            days.Add(new DateTime(2023, 2, 14, 7, 30, 0));

            for (int i = 0; i < 10000; i++)
            {
                var randomday = days[new Random().Next(0, days.Count - 1)];
                var time = randomday.AddMinutes(new Random().Next(0, 500));

                var randomVal = new Random().Next(1, 5);

                query += $"INSERT INTO MoodEntry (`Date`,MoodDeviceId,MoodFaceId) VALUES ('{time.ToString("yyyy-MM-dd H:mm:ss")}','1','{randomVal}');" + Environment.NewLine;
            }


            return new OkObjectResult(query);
        }


        [FunctionName("GetChartData")]
        public static async Task<IActionResult> GetChartData([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequest req)
        {
            var data = DbHelper.GetChartData();
            return new OkObjectResult(data);
        }
    }

    public class ChartData
    {
        public ChartData(int face1Count, int face2Count, int face3Count, int face4Count, DateTime date)
        {
            Face1Count = face1Count;
            Face2Count = face2Count;
            Face3Count = face3Count;
            Face4Count = face4Count;
            Date = date.ToString("yyyy-MM-dd");
        }

        public int Face1Count { get; set; }

        public int Face2Count { get; set; }

        public int Face3Count { get; set; }

        public int Face4Count { get; set; }

        public string Date { get; set; }
    }
}
