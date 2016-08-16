using System;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;

namespace AntiVirusWebApi.Scanners
{
	using Models;

	public class WinDefenderScanner
		: IScanner
	{
		public Detection ScanByFilePath(string filePath)
		{
			// TODO: Use IOC to inject Windows Defender info
			const string winDefenderFilePath = @"C:\Program Files\Windows Defender\mpcmdrun.exe";

			// Start the child process.
			Process process = new Process
			{
				StartInfo =
				{
					UseShellExecute = false,
					RedirectStandardOutput = true,
					FileName = winDefenderFilePath,
					Arguments = $"-scan -scantype 3 -file \"{filePath}\" -DisableRemediation"
				}
			};
			process.Start();
			string output = process.StandardOutput.ReadToEnd();
			process.WaitForExit();

			Detection detection = new Detection
			{
				AvVendor = AvVendor.WindowsDefender,
				Type = DetectionType.None
			};

			string pattern = @"Threat *:.*";
			Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
			Match match = regex.Match(output);
			if (!match.Success)
				return detection;

			string[] matchingThreat = match
				.Groups[0]
				.ToString()
				.Split(':')
				.Select(str => str.Trim())
				.ToArray();

			if (matchingThreat.Length != 3)
			{
				detection.Type = DetectionType.Error;
				detection.Message = "Unexpected output";
				return detection;
			}

			if (string.Equals(matchingThreat[1], "virus", StringComparison.OrdinalIgnoreCase))
				detection.Type = DetectionType.Virus;
			else if (string.Equals(matchingThreat[1], "trojan", StringComparison.OrdinalIgnoreCase))
				detection.Type = DetectionType.Trojan;
			else
				detection.Type = DetectionType.Other;

			detection.Name = matchingThreat[2];

			return detection;
		}
	}
}