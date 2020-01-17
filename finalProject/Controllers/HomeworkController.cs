using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using finalProject.Models;
using  VoiceRSS_SDK;
using System.Web.UI;
using System.IO;

namespace finalProject.Controllers
{
    public class HomeworkController : Controller
    {


        private List<string> GetFiles()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/homeworks"));
            System.IO.FileInfo[] fileName = dir.GetFiles("*.mp3");
            List<string> items = new List<string>();
            foreach (var file in fileName)
            {

                items.Add(file.Name);
            }
            return items;
        }

        public FileResult Download(string mp3name)
        {
            var filevar = "~/homeworks/" + mp3name;
            return File(filevar, "application/force- download", Path.GetFileName(filevar));

        }
        public ActionResult showHomeworks()
        {
            var item = GetFiles();
            return View(item);
        }

        class TextToVoiceUtil
        {
            public static void convert(Homework var)
            {
                var apiKey = "0c72079c4af84be48272ecd791d372cb";
                var isSSL = false;
                var text = var.homeWork1;
                var lang = Languages.English_UnitedStates;
                string file_name = (var.homeworknumber).ToString() + ".mp3";



                var voiceParams = new VoiceParameters(text, lang)
                {
                    AudioCodec = AudioCodec.MP3,
                    AudioFormat = AudioFormat.Format_44KHZ.AF_44khz_16bit_stereo,
                    IsBase64 = false,
                    IsSsml = false,
                    SpeedRate = 0
                };

                var voiceProvider = new VoiceProvider(apiKey, isSSL);
                var voice = voiceProvider.Speech<byte[]>(voiceParams);



                var fileName = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/homeworks"), file_name);
                System.IO.File.WriteAllBytes(fileName, voice);


            }
        }



        private IdentityDBEntities9 db = new IdentityDBEntities9();

        // GET: Homework
        public ActionResult Index()
        {
            return View(db.Homework.ToList());
        }

        // GET: Homework/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homework homework = db.Homework.Find(id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            return View(homework);
        }

        // GET: Homework/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Homework/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "homeworknumber,homeworktitle,homeWork1")] Homework homework)
        {
            TextToVoiceUtil.convert(homework);
            if (ModelState.IsValid)
            {
                db.Homework.Add(homework);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(homework);
        }

        // GET: Homework/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homework homework = db.Homework.Find(id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            return View(homework);
        }

        // POST: Homework/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "homeworknumber,homeworktitle,homeWork1")] Homework homework)
        {
            if (ModelState.IsValid)
            {
                db.Entry(homework).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(homework);
        }

        // GET: Homework/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Homework homework = db.Homework.Find(id);
            if (homework == null)
            {
                return HttpNotFound();
            }
            return View(homework);
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Homework homework = db.Homework.Find(id);
            db.Homework.Remove(homework);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
