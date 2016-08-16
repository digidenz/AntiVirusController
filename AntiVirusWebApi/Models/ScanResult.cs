using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace AntiVirusWebApi.Models
{
	[Serializable]
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
		public bool MalwareDetected
		{
			get
			{
				return Detections.Any(
					detection =>
						detection.Type == DetectionType.Virus ||
						detection.Type == DetectionType.Trojan ||
						detection.Type == DetectionType.Other
				);
			}
		}

		[DataMember]
		public bool ErrorDetected
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