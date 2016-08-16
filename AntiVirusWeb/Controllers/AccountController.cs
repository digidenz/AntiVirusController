using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using AntiVirusWeb.Models;
using log4net;

namespace AntiVirusWeb.Controllers
{
	public class AccountController : Controller
	{
        protected ILog Log;
        
        public AccountController()
        {
            Log = log4net.LogManager.GetLogger(this.GetType());
        }

        // GET: Scan
        public ActionResult Index()
		{
            Log.Info("Index page is being accessed");
            return RedirectToAction("Login");
        }

        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Error = "Form is not valid; please review and try again.";
                Log.Info("User credential is not passed correctly.");
                return View("Login");
            }

           // if (login.Username == "TestUser" && login.Password == "Welcome1")
                FormsAuthentication.RedirectFromLoginPage(login.Username, true);

            ViewBag.Error = "Credentials invalid. Please try again.";
            Log.Info("User Credential is not correct.");
            return View("Login");
        }
    }
}