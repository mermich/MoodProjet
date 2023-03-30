using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MoodProjet.Auth;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MoodProjet.Devices
{
	public static class DevicesFunction
	{
		[OpenApiOperation(operationId: "Devices-List", tags: new[] { "Devices" })]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json; charset=utf-8", bodyType: typeof(List<Device>), Description = "The OK response")]
		[FunctionName("Devices-List")]
		public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Devices")] HttpRequest req, ILogger logger)
		{
			// %TMP%\LogFiles\Application\Functions
			logger.LogWarning("test");
			List<Device> devices = DevicesDataManager.ListDevices();
			return new OkObjectResult(devices);
		}

		[OpenApiOperation(operationId: "Devices-Get", tags: new[] { "Devices" })]
		[OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The **id** parameter")]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json; charset=utf-8", bodyType: typeof(Device), Description = "The OK response")]
		[FunctionName("Devices-Get")]
		public static IActionResult GetDevice([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Devices/{id}")] HttpRequest req, int id)
		{
			return new OkObjectResult(DevicesDataManager.ListDevices().FirstOrDefault(c => c.Id == id));
		}

		[OpenApiOperation(operationId: "Devices-Save", tags: new[] { "Devices" })]
		[OpenApiRequestBody(contentType: "application/json; charset=utf-8", bodyType: typeof(Device), Description = "", Required = true)]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json; charset=utf-8", bodyType: typeof(Device), Description = "The OK response")]
		[FunctionName("Devices-Save")]
		public static async Task<IActionResult> SaveDevice([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Devices")] HttpRequest req)
		{
			if (JwtHelper.CheckPermissionAndExpiration(req, JwtHelper.CanAdminDevices))
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

		[OpenApiOperation(operationId: "Devices-Update", tags: new[] { "Devices" })]
		[OpenApiRequestBody(contentType: "application/json; charset=utf-8", bodyType: typeof(Device), Description = "", Required = true)]
		[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json; charset=utf-8", bodyType: typeof(Device), Description = "The OK response")]
		[FunctionName("Devices-Update")]
		public static async Task<IActionResult> UpdateDevice([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "Devices")] HttpRequest req, ILogger log)
		{
			if (JwtHelper.CheckPermissionAndExpiration(req, JwtHelper.CanAdminDevices))
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

		[OpenApiOperation(operationId: "Devices-Delete", tags: new[] { "Devices" })]
		[OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(int), Description = "The **id** parameter")]
		[FunctionName("Devices-Delete")]
		public static IActionResult DeleteDevice([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "Devices/{id}")] HttpRequest req, int id, ILogger log)
		{
			if (JwtHelper.CheckPermissionAndExpiration(req, JwtHelper.CanAdminDevices))
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
