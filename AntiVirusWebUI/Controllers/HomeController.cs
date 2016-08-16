using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace AntiVirusWebUI.Controllers
{
    public class HomeController : Controller
    {
        protected ILog Log;

        public HomeController()
        {
            Log = log4net.LogManager.GetLogger(this.GetType());
        }

        public ActionResult Index()
        {
            Log.Info("Index page is being accessed");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            
            Log.Info("About us page is being accessed");

            return View();
        }

        public ActionResult Contact()
        {
            Log.Info("Contact us page is being accessed");
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}