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
    public static class MoodDevicesFunction
    {
        [FunctionName("Devices-List")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Devices")] HttpRequest req)
        {
            var etudiants = DbHelper.ListDevices();
            return new OkObjectResult(etudiants);
        }

        [FunctionName("Devices-Get")]
        public static async Task<IActionResult> GetDevice([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Devices/{id}")] HttpRequest req, int id)
        {
            return new OkObjectResult(DbHelper.ListDevices().FirstOrDefault(c => c.Id == id));
        }

        [FunctionName("Devices-Save")]
        public static async Task SaveDevice([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Devices")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Device device = JsonConvert.DeserializeObject<Device>(requestBody);

            DbHelper.RunCommand($"INSERT INTO Device (Label, IsActive) VALUES ('{device.Label}', {device.IsActive});");
        }

        [FunctionName("Devices-Update")]
        public static async Task UpdateDevice([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Devices")] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Device device = JsonConvert.DeserializeObject<Device>(requestBody);

            DbHelper.RunCommand($"UPDATE Device SET Label = '{device.Label}', IsActive = {device.IsActive} WHERE Id = {device.Id};");
        }

        [FunctionName("Devices-Delete")]
        public static Task DeleteDevice([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Devices/{id}")] HttpRequest req, int id, ILogger log)
        {
            DbHelper.RunCommand($"DELETE FROM Device WHERE Id={id};");
            return Task.CompletedTask;
        }
    }
}
