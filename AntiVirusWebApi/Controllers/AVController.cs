using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using AntiVirusWebApi.Models;
using AntiVirusWebApi.Scanners;

namespace AntiVirusWebApi.Controllers
{
	[RoutePrefix("api/avscan")]
	public class AVController
		: ApiController
	{
		public AVController()
		{
		}

		[HttpGet, Route("byfilepath")]
		public IHttpActionResult ScanFile(string filePath)
		{
			ScanResult scanResult = new ScanResult
			{
				ScannedFile = filePath
			};

			// TODO: Use IOC to resolve scanners
			IList<IScanner> scanners = new List<IScanner>
			{
				new ClamAvScanner(),
				new WinDefenderScanner()
			};

			scanResult.Detections.AddRange(
				scanners
					.Select(
						scanner => scanner.ScanByFilePath(filePath)
					)
				);

			return Ok(scanResult);
		}
	}
}