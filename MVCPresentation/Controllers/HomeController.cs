using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentation.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "ComicsOnline is an easy way to order your comics for pickup.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us";

            return View();
        }

        [Authorize(Roles = "Employee")]
        public ActionResult Customer()
        {
            ViewBag.Message = "Current customers";

            return View();
        }
    }
}