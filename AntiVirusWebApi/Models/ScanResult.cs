using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AntiVirusWebApi.Models
{
	[DataContract]
	public class ScanResult
	{
		readonly List<Detection> _detections = new List<Detection>();

		[DataMember]
		public string ScannedFile
		{
			get;
			set;
		}

		[DataMember]
		public List<Detection> Detections => _detections;

		[DataMember]
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

		[DataMember]
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