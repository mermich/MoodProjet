using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MoodProjet.Devices;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoodProjet.MoodDevices
{
    public static class DevicesFunction
    {

        [FunctionName("Devices-List")]
        public static async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Devices")] HttpRequest req)
        {
            List<Device> etudiants = DevicesDataManager.ListDevices();
            return new OkObjectResult(etudiants);
        }

        [FunctionName("Devices-Get")]
        public static async Task<IActionResult> GetDevice([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Devices/{id}")] HttpRequest req, int id)
        {
            return new OkObjectResult(DevicesDataManager.ListDevices().FirstOrDefault(c => c.Id == id));
        }

        [FunctionName("Devices-Save")]
        public static async Task SaveDevice([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Devices")] HttpRequest req)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Device device = JsonConvert.DeserializeObject<Device>(requestBody);

            DevicesDataManager.SaveDevice(device);
        }

        [FunctionName("Devices-Update")]
        public static async Task UpdateDevice([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Devices")] HttpRequest req, ILogger log)
        {
            string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            Device device = JsonConvert.DeserializeObject<Device>(requestBody);

            DevicesDataManager.UpdateDevice(device);
        }

        [FunctionName("Devices-Delete")]
        public static Task DeleteDevice([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Devices/{id}")] HttpRequest req, int id, ILogger log)
        {
            DevicesDataManager.DeleteDevice(id);
            return Task.CompletedTask;
        }
    }
}
