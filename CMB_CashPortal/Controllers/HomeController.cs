using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CMB_CashPortal.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Lobby()
        {
            return View();
        }

       public ActionResult LandingPage()
        {
            return View();
        }
    }
}