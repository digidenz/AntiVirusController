using System.Linq;
using System.Web.Http;
using nClam;

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
			// TODO: Use IOC to set server config
			ClamClient clam = new ClamClient("localhost", 3310);
			ClamScanResult scanResult = clam.ScanFileOnServer(filePath);

			// TODO: Return proper structured object
			switch (scanResult.Result)
			{
				case ClamScanResults.Clean:
					return Ok("File is clean!");
				case ClamScanResults.VirusDetected:
					return Ok($"Warning! File is infected by: '{scanResult.InfectedFiles.First().VirusName}'");
				case ClamScanResults.Error:
					return BadRequest($"An error occured when scanning the file: '{scanResult.RawResult}'");
				default:
					return BadRequest($"Expected scan result: '{scanResult.Result}'");
			}
		}
	}
}