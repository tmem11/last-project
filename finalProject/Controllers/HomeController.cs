using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace finalProject.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Roles = "Students")]
        public ActionResult StudentsPages()
        {
            return View();
        }
        [Authorize(Roles = "Parents")]
        public ActionResult ParentPages()
        {
            return View();
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [Authorize(Roles = "admins")]

        public ActionResult Adminpages()
        {
            return View();
        }
        [Authorize(Roles = "Teachers")]
        public ActionResult TeachersPages()
        {
            return View();
        }

    }
}