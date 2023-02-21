using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MoodProjet.Devices;
using MoodProjet.MoodFaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoodProjet.MoodEntries
{
    public static class MoodEntriesFunction
    {
        [FunctionName("MoodEntries-List")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "MoodEntries")] HttpRequest req)
        {
            List<MoodEntry> moodEntrys = MoodEntriesDataManager.ListMoodEntries().Take(100).ToList();

            if (req.Query.ContainsKey("includes"))
            {
                string includes = req.Query["includes"].FirstOrDefault();

                if (!string.IsNullOrWhiteSpace(includes))
                {
                    List<string> includeArray = includes.Split(',').ToList();
                    if (includeArray.Contains("moodFace"))
                    {
                        List<MoodFace> faces = MoodFacesDataManager.ListMoodFaces();
                        moodEntrys = moodEntrys.Select(m => new MoodEntry(m.Id, m.MoodFaceId, m.Date, m.MoodDeviceId, faces.FirstOrDefault(f => f.Id == m.MoodFaceId), m.device)).ToList();
                    }

                    if (includeArray.Contains("device"))
                    {
                        List<Device> devices = DevicesDataManager.ListDevices();
                        moodEntrys = moodEntrys.Select(m => new MoodEntry(m.Id, m.MoodFaceId, m.Date, m.MoodDeviceId, m.moodFace, devices.FirstOrDefault(f => f.Id == m.MoodDeviceId))).ToList();
                    }
                }
            }

            return new OkObjectResult(moodEntrys);
        }

        [FunctionName("MoodEntries-Get")]
        public static async Task<IActionResult> GetMoodEntry([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "MoodEntries/{id}")] HttpRequest req, int id)
        {
            return new OkObjectResult(MoodEntriesDataManager.ListMoodEntries().FirstOrDefault(c => c.Id == id));
        }

        [FunctionName("MoodEntries-Save")]
        public static async Task SaveMoodEntry([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "MoodEntries")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            MoodEntry moodEntry = JsonConvert.DeserializeObject<MoodEntry>(requestBody);

            MoodEntriesDataManager.SaveMoodEntry(moodEntry);
        }

        [FunctionName("MoodEntries-Update")]
        public static async Task UpdateMoodEntry([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "MoodEntries")] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            MoodEntry moodEntry = JsonConvert.DeserializeObject<MoodEntry>(requestBody);

            MoodEntriesDataManager.UpdateMoodEntry(moodEntry);
        }

        [FunctionName("MoodEntries-Delete")]
        public static Task DeleteMoodEntry([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "MoodEntries/{id}")] HttpRequest req, int id, ILogger log)
        {
            MoodEntriesDataManager.DeleteMoodEntry(id);
            return Task.CompletedTask;
        }
    }
}
