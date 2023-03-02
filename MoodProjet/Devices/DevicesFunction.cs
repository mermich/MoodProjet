using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MoodProjet.Auth;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MoodProjet.Devices
{
    public static class DevicesFunction
    {

        [FunctionName("Devices-List")]
        public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Devices")] HttpRequest req)
        {
            List<Device> devices = DevicesDataManager.ListDevices();
            return new OkObjectResult(devices);
        }

        [FunctionName("Devices-Get")]
        public static IActionResult GetDevice([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Devices/{id}")] HttpRequest req, int id)
        {
            return new OkObjectResult(DevicesDataManager.ListDevices().FirstOrDefault(c => c.Id == id));
        }

        [FunctionName("Devices-Save")]
        public static async Task<IActionResult> SaveDevice([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Devices")] HttpRequest req)
        {
            bool canAdminDevices = JwtHelper.GetClaimAsBool(req, JwtHelper.CanAdminDevices);
            if (canAdminDevices)
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Device device = JsonConvert.DeserializeObject<Device>(requestBody);

                DevicesDataManager.SaveDevice(device);
                return new OkObjectResult(device);
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [FunctionName("Devices-Update")]
        public static async Task<IActionResult> UpdateDevice([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Devices")] HttpRequest req, ILogger log)
        {
            bool canAdminDevices = JwtHelper.GetClaimAsBool(req, JwtHelper.CanAdminDevices);
            if (canAdminDevices)
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                Device device = JsonConvert.DeserializeObject<Device>(requestBody);

                DevicesDataManager.UpdateDevice(device);
                return new OkObjectResult(device);
            }
            else
            {
                return new BadRequestResult();
            }
        }

        [FunctionName("Devices-Delete")]
        public static IActionResult DeleteDevice([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Devices/{id}")] HttpRequest req, int id, ILogger log)
        {
            bool canAdminDevices = JwtHelper.GetClaimAsBool(req, JwtHelper.CanAdminDevices);
            if (canAdminDevices)
            {
                DevicesDataManager.DeleteDevice(id);
                return new OkObjectResult(id);
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}
