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

namespace MoodProjet.MoodFaces
{
	public static class MoodFaceFunction
	{
		[FunctionName("MoodFaces-List")]
		public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "MoodFaces")] HttpRequest req)
		{
			List<MoodFace> moddFaces = MoodFacesDataManager.ListMoodFaces().OrderBy(f => f.Key).ToList();
			return new OkObjectResult(moddFaces);
		}

		[FunctionName("MoodFaces-Get")]
		public static IActionResult GetMoodFace([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "MoodFaces/{id}")] HttpRequest req, int id)
		{
			return new OkObjectResult(MoodFacesDataManager.ListMoodFaces().FirstOrDefault(c => c.Id == id));
		}

		[FunctionName("MoodFaces-Save")]
		public static async Task<IActionResult> SaveMoodFace([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "MoodFaces")] HttpRequest req)
		{
			bool canAdminDevices = JwtHelper.GetClaimAsBool(req, JwtHelper.CanAdminMoodFaces);
			if (canAdminDevices)
			{
				string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
				MoodFace moodFace = JsonConvert.DeserializeObject<MoodFace>(requestBody);

				MoodFacesDataManager.SaveMoodFace(moodFace);
				return new OkObjectResult(moodFace);
			}
			else
			{
				return new BadRequestResult();
			}
		}

		[FunctionName("MoodFaces-Update")]
		public static async Task<IActionResult> UpdateMoodFace([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "MoodFaces")] HttpRequest req, ILogger log)
		{
			bool canAdminDevices = JwtHelper.GetClaimAsBool(req, JwtHelper.CanAdminMoodFaces);
			if (canAdminDevices)
			{
				string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
				MoodFace moodFace = JsonConvert.DeserializeObject<MoodFace>(requestBody);

				MoodFacesDataManager.UpdateMoodFace(moodFace);
				return new OkObjectResult(moodFace);
			}
			else
			{
				return new BadRequestResult();
			}
		}

		[FunctionName("MoodFaces-Delete")]
		public static IActionResult DeleteMoodFace([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "MoodFaces/{id}")] HttpRequest req, int id, ILogger log)
		{
			bool canAdminDevices = JwtHelper.GetClaimAsBool(req, JwtHelper.CanAdminMoodFaces);
			if (canAdminDevices)
			{
				MoodFacesDataManager.DeleteMoodFace(id);
				return new OkObjectResult(id);
			}
			else
			{
				return new BadRequestResult();
			}
		}
	}
}
