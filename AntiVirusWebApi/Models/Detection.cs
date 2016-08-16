using System;
using System.Runtime.Serialization;

namespace AntiVirusWebApi.Models
{
	[Serializable]
	[DataContract]
	public class Detection
	{
		[DataMember]
		public AvVendor AvVendor
		{
			get;
			set;
		}

		[DataMember]
		public DetectionType Type
		{
			get;
			set;
		}

		[DataMember]
		public string Name
		{
			get;
			set;
		}

		[DataMember]
		public string Message
		{
			get;
			set;
		}
	}
}