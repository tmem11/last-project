using System;
using System.Linq;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace finalProject.Controllers
{
    public class RecordedbookController : Controller
    {
        // GET: Recordedbook
        public ActionResult Index()
        {
            var items = GetFiles();
            return View(items);
        }
        [HttpPost]
        public ActionResult Index(HttpPostedFileBase file)


        {
            if (file != null && file.ContentLength > 0)
                try
                {

                    string path = Path.Combine(Server.MapPath("~/books"), Path.GetFileName(file.FileName));
                    file.SaveAs(path);
                    ViewBag.Message = "File upload successfully";


                }
                catch (Exception ex)
                {
                    ViewBag.Message = "eroor:" + ex.Message.ToString();

                }
            else
            {

                ViewBag.Message = "you have not specifile file";


            }
            var items = GetFiles();
            return View(items);


        }





        public FileResult Download(string ImageName)
        {
            var filevar = "~/books/" + ImageName;
            return File(filevar, "application/force- download", Path.GetFileName(filevar));

        }
        public ActionResult showfiles()
        {
            var items = GetFiles();
            return View(items);
        }

        private List<string> GetFiles()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/books"));
            System.IO.FileInfo[] fileName = dir.GetFiles("*.*");
            List<string> items = new List<string>();
            foreach (var file in fileName)
            {

                items.Add(file.Name);
            }
            return items;
        }


    }
}