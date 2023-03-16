using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace MoodProjet.Init
{
	public static class InitFunction
	{
		[FunctionName("Init-CheckDB")]
		public static IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "CheckDB")] HttpRequest req)
		{
			return new OkObjectResult(InitDataManager.CheckDbConn());
		}

		[FunctionName("Init-InitDB")]
		public static IActionResult InitDB([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "InitDB")] HttpRequest req)
		{
			return new OkObjectResult(InitDataManager.InitDB());
		}


		[FunctionName("Init-SetConfigurationTables")]
		public static IActionResult SetConfigurationTables([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "SetConfigurationTables")] HttpRequest req)
		{
			return new OkObjectResult(InitDataManager.SetConfigurationTables());
		}




		[FunctionName("Init-SetEntriesTables")]
		public static IActionResult UpdateMoodEntry([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "SetEntriesTables")] HttpRequest req, ILogger log)
		{
			return new OkObjectResult(InitDataManager.SetEntriesTables());
		}

		[FunctionName("Init-SetUsers")]
		public static IActionResult SetUsers([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "SetUsers")] HttpRequest req, ILogger log)
		{
			return new OkObjectResult(InitDataManager.SetUsers());
		}



		[FunctionName("Init-KickStart")]
		public static IActionResult KickStart([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "KickStart")] HttpRequest req, ILogger log)
		{
			var res = new
			{
				CheckDbConn = InitDataManager.CheckDbConn(),
				InitDB = InitDataManager.InitDB(),
				SetConfigurationTables = InitDataManager.SetConfigurationTables(),
				SetEntriesTables = InitDataManager.SetEntriesTables(),
				SetUsers = InitDataManager.SetUsers(),
			};

			return new OkObjectResult(res);
		}

	}
}
