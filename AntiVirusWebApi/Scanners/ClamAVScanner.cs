using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AntiVirusWebApi.Models;
using nClam;

namespace AntiVirusWebApi.Scanners
{
	public class ClamAvScanner
		: IScanner
	{
		// TODO: Use IOC to inject server info
		readonly ClamClient _clamClient = new ClamClient("localhost", 3310);

		public Detection ScanByFilePath(string filePath)
		{
			Detection detection = new Detection
			{
				AvVendor = AvVendor.ClamAv,
				Type = DetectionType.None
			};
			ClamScanResult scanResult = _clamClient.ScanFileOnServer(filePath);
			switch (scanResult.Result)
			{
				case ClamScanResults.Clean:
					break;
				case ClamScanResults.VirusDetected:
					detection.Name = scanResult.InfectedFiles.First().VirusName;
					detection.Type = DetectionType.Virus;
					break;
				case ClamScanResults.Error:
					detection.Type = DetectionType.Error;
					detection.Message = scanResult.RawResult;
					break;
				default:
					detection.Type = DetectionType.Error;
					detection.Message = $"Unexpected scan result: '{scanResult.Result}'";
					break;
			}

			return detection;
		}
	}
}