namespace AntiVirusWebApi.Models
{
	public class Detection
	{
		public AvVendor AvVendor
		{
			get;
			set;
		}

		public DetectionType Type
		{
			get;
			set;
		}

		public string Name
		{
			get;
			set;
		}

		public string Message
		{
			get;
			set;
		}
	}
}