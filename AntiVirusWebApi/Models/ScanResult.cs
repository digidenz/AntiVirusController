using System.Collections.Generic;
using System.Linq;

namespace AntiVirusWebApi.Models
{
	public class ScanResult
	{
		readonly List<Detection> _detections = new List<Detection>();

		public string ScannedFile
		{
			get;
			set;
		}

		public List<Detection> Detections => _detections;

		bool MalwareDetected
		{
			get
			{
				return Detections.Any(
					detection =>
						detection.Type == DetectionType.Virus ||
						detection.Type == DetectionType.Trojan
				);
			}
		}

		bool ErrorDetected
		{
			get
			{
				return Detections.Any(
					detection =>
						detection.Type == DetectionType.Error
				);
			}
		}
	}
}