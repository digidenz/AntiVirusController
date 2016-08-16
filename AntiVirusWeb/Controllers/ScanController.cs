using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FileScanner.Controllers
{
	public class ScanController : Controller
	{
		// GET: Scan
		public ActionResult Index()
		{
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
		public ActionResult UploadFile(HttpPostedFileBase postedFile)
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

			return Json("Successfully upload");
		}
	}
}