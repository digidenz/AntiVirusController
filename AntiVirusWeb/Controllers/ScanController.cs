using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using log4net;
using AntiVirusWebApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace AntiVirusWeb.Controllers
{
	public class ScanController : Controller
	{
		/// <summary>
		/// The default json serializer settings.
		/// </summary>
		private static readonly JsonSerializerSettings DefaultJsonSerializerSettings =
			new JsonSerializerSettings
			{
				Converters =
					new List<JsonConverter>
						{
							new StringEnumConverter()
						}
			};
		protected ILog Log;
        
        public ScanController()
        {
            Log = log4net.LogManager.GetLogger(this.GetType());
        }

        // GET: Scan
        [Authorize]
        public ActionResult Index()
		{
            Log.Info("Index page is being accessed");
            return View();
		}

		/// <summary>
		/// 		The upload file action.
		/// </summary>
		/// <param name="postedFile">
		/// 		The posted file.
		/// </param>
		/// <returns>
		/// 		The <see cref="Task"/>.
		/// </returns>
		[System.Web.Mvc.HttpPost]
		[Authorize]
		public async Task<ActionResult> UploadFile(HttpPostedFileBase postedFile)
		{
			// file validation.
			if (postedFile.ContentLength > int.Parse(ConfigurationManager.AppSettings["FileMaxUploadSizeBytes"]))
			{
				throw new ArgumentException("File exceeds the max file size limit");
			}

			string fileExt = Path.GetExtension(postedFile.FileName);

			// save file
			var newFileName = Path.ChangeExtension(Path.GetRandomFileName(), fileExt);

			string fileLocation = ConfigurationManager.AppSettings["FileUploadLocation"];

			if (string.IsNullOrWhiteSpace(fileLocation))
				throw new ArgumentException("Invalid FileUploadLocation in web.config");

			var filePath = Path.Combine(fileLocation, newFileName);

			// Delete if the file exists already
			if (System.IO.File.Exists(filePath))
				System.IO.File.Delete(filePath);

			postedFile.SaveAs(filePath);

			var scanResult = await ScanFile(filePath);

			return Json(scanResult);
		}

		private async Task<ScanResult> ScanFile(string filePath)
		{
			using (var client = new HttpClient())
			{
				client.BaseAddress = new Uri("http://localhost:51810/");
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

				// New code:
				HttpResponseMessage response = await client.GetAsync("api/avscan/byfilepath?filePath=" + filePath);
				if (response.IsSuccessStatusCode)
				{
					//ScanResult scanResult = JsonSerializer response.Content.ReadAsStringAsync();
					var responseContent = await response.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<ScanResult>(responseContent, DefaultJsonSerializerSettings);
				}
			}

			return null;
		}
	}
}