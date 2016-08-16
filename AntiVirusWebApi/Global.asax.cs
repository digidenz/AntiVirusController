using System.Net;
using System.Web;
using System.Web.Http;

namespace AntiVirusWebApi
{
	public class WebApiApplication
		: HttpApplication
	{
		protected void Application_Start()
		{
			GlobalConfiguration.Configure(WebApiConfig.Register);

            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }

            log4net.GlobalContext.Properties["IPAddress"] = localIP;
        }
	}
}
