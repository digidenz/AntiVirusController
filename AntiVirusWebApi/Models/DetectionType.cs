using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AntiVirusWebApi.Models
{
	public enum DetectionType
	{
		Unknown = 0,
		None = 1,
		Virus = 2,
		Trojan = 3,
		Error = 100
	}
}