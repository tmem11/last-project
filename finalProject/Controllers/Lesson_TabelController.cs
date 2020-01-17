using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using finalProject.Models;
using VoiceRSS_SDK;
using System.Web.UI;
using System.IO;
using System;

namespace finalProject.Controllers
{
    public class Lesson_TabelController : Controller
    {

        private List<string> GetFiles()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Lesson"));
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
            var filevar = "~/Lesson/" + mp3name;
            return File(filevar, "application/force- download", Path.GetFileName(filevar));

        }
        public ActionResult showLesson()
        {
            var item = GetFiles();
            return View(item);
        }

        class TextToVoiceUtil
        {
            public static void convert(Lesson_Tabel var)
            {
                var apiKey = "0c72079c4af84be48272ecd791d372cb";
                var isSSL = false;
                var text = var.Lesson;
                var lang = Languages.English_UnitedStates;
                string file_name = (var.LessonNumber).ToString() + ".mp3";



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



                var fileName = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Lesson"), file_name);
                System.IO.File.WriteAllBytes(fileName, voice);


            }
        }


        private IdentityDBEntities10 db = new IdentityDBEntities10();

        // GET: Lesson_Tabel
        public ActionResult Index(string searchString)
        {
            var lesson_req = from s in db.Lesson_Tabel
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                lesson_req = lesson_req.Where(s => s.LessonNumber.Contains(searchString)
                                       );
            }
            return View(lesson_req);
        }

        // GET: Lesson_Tabel/Details/5
        public ActionResult Detail(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson_Tabel lesson_Tabel = db.Lesson_Tabel.Find(id);
            if (lesson_Tabel == null)
            {
                return HttpNotFound();
            }
            return View(lesson_Tabel);
        }

        // GET: Lesson_Tabel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Lesson_Tabel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "LessonNumber,LessonTitle,Lesson")] Lesson_Tabel Lesson_Tabel)
        {
            TextToVoiceUtil.convert(Lesson_Tabel);
            if (ModelState.IsValid)
            {
                db.Lesson_Tabel.Add(Lesson_Tabel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(Lesson_Tabel);
        }

        // GET: Lesson_Tabel/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson_Tabel lesson_Tabel = db.Lesson_Tabel.Find(id);
            if (lesson_Tabel == null)
            {
                return HttpNotFound();
            }
            return View(lesson_Tabel);
        }

        // POST: Lesson_Tabel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "LessonNumber,LessonTitle,Lesson")] Lesson_Tabel lesson_Tabel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(lesson_Tabel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(lesson_Tabel);
        }

        // GET: Lesson_Tabel/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Lesson_Tabel lesson_Tabel = db.Lesson_Tabel.Find(id);
            if (lesson_Tabel == null)
            {
                return HttpNotFound();
            }
            return View(lesson_Tabel);
        }

        // POST: Lesson_Tabel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Lesson_Tabel lesson_Tabel = db.Lesson_Tabel.Find(id);
            db.Lesson_Tabel.Remove(lesson_Tabel);
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
