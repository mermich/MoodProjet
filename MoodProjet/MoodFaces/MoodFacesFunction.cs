using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoodProjet.MoodFaces
{
    public static class MoodFaceFunction
    {
        [FunctionName("MoodFaces-List")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "MoodFaces")] HttpRequest req)
        {
            List<MoodFace> etudiants = MoodFacesDataManager.ListMoodFaces().OrderBy(f => f.Key).ToList();
            return new OkObjectResult(etudiants);
        }

        [FunctionName("MoodFaces-Get")]
        public static async Task<IActionResult> GetMoodFace([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "MoodFaces/{id}")] HttpRequest req, int id)
        {
            return new OkObjectResult(MoodFacesDataManager.ListMoodFaces().FirstOrDefault(c => c.Id == id));
        }

        [FunctionName("MoodFaces-Save")]
        public static async Task SaveMoodFace([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "MoodFaces")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            MoodFace moodFace = JsonConvert.DeserializeObject<MoodFace>(requestBody);

            MoodFacesDataManager.SaveMoodFace(moodFace);
        }

        [FunctionName("MoodFaces-Update")]
        public static async Task UpdateMoodFace([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "MoodFaces")] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            MoodFace moodFace = JsonConvert.DeserializeObject<MoodFace>(requestBody);

            MoodFacesDataManager.UpdateMoodFace(moodFace);
        }

        [FunctionName("MoodFaces-Delete")]
        public static Task DeleteMoodFace([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "MoodFaces/{id}")] HttpRequest req, int id, ILogger log)
        {
            MoodFacesDataManager.DeleteMoodFace(id);
            return Task.CompletedTask;
        }
    }
}
