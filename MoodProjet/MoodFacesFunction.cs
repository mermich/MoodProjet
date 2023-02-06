using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MoodProjet.db;
using Newtonsoft.Json;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoodProjet
{
    public static class MoodFaceFunction
    {
        [FunctionName("MoodFaces-List")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "MoodFaces")] HttpRequest req)
        {
            var etudiants = DbHelper.ListMoodFaces().OrderBy(f => f.Key).ToList();
            return new OkObjectResult(etudiants);
        }

        [FunctionName("MoodFaces-Get")]
        public static async Task<IActionResult> GetMoodFace([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "MoodFaces/{id}")] HttpRequest req, int id)
        {
            return new OkObjectResult(DbHelper.ListMoodFaces().FirstOrDefault(c => c.Id == id));
        }

        [FunctionName("MoodFaces-Save")]
        public static async Task SaveMoodFace([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "MoodFaces")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            MoodFace moodFace = JsonConvert.DeserializeObject<MoodFace>(requestBody);

            DbHelper.RunCommand($"INSERT INTO MoodFace (`Key`, Picture, Label, IsActive) VALUES ('{moodFace.Key}','{moodFace.Picture}','{moodFace.Label}',{moodFace.IsActive});");
        }

        [FunctionName("MoodFaces-Update")]
        public static async Task UpdateMoodFace([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "MoodFaces")] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            MoodFace moodFace = JsonConvert.DeserializeObject<MoodFace>(requestBody);

            DbHelper.RunCommand($"UPDATE MoodFace SET `Key` = '{moodFace.Key}', Picture = '{moodFace.Picture}', Label = '{moodFace.Label}', IsActive = {moodFace.IsActive} WHERE Id = {moodFace.Id};");
        }

        [FunctionName("MoodFaces-Delete")]
        public static Task DeleteMoodFace([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "MoodFaces/{id}")] HttpRequest req, int id, ILogger log)
        {
            DbHelper.RunCommand($"DELETE FROM MoodFace WHERE Id={id};");
            return Task.CompletedTask;
        }
    }
}
