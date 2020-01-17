using System;
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


namespace finalProject.Controllers
{
    public class Exam_TabelController : Controller
    {
        private List<string> GetFiles()
        {
            var dir = new System.IO.DirectoryInfo(Server.MapPath("~/Exam"));
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
            var filevar = "~/Exam/" + mp3name;
            return File(filevar, "application/force- download", Path.GetFileName(filevar));

        }
        public ActionResult showExam()
        {
            var item = GetFiles();
            return View(item);
        }

        class TextToVoiceUtil
        {
            public static void convert(Exam_Tabel var)
            {
                var apiKey = "0c72079c4af84be48272ecd791d372cb";
                var isSSL = false;
                var text = var.Exam;
                var lang = Languages.English_UnitedStates;
                string file_name = (var.Examid).ToString() + ".mp3";



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



                var fileName = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~/Exam"), file_name);
                System.IO.File.WriteAllBytes(fileName, voice);


            }
        }


        private IdentityDBEntities14 db = new IdentityDBEntities14();

        // GET: Exam_Tabel
        public ActionResult Index()
        {
            return View(db.Exam_Tabel.ToList());
        }

        // GET: Exam_Tabel/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam_Tabel exam_Tabel = db.Exam_Tabel.Find(id);
            if (exam_Tabel == null)
            {
                return HttpNotFound();
            }
            return View(exam_Tabel);
        }

        // GET: Exam_Tabel/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Exam_Tabel/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Examid,Exam")] Exam_Tabel exam_Tabel)
        {
            TextToVoiceUtil.convert(exam_Tabel);
            if (ModelState.IsValid)
            {
                db.Exam_Tabel.Add(exam_Tabel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(exam_Tabel);
        }

        // GET: Exam_Tabel/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam_Tabel exam_Tabel = db.Exam_Tabel.Find(id);
            if (exam_Tabel == null)
            {
                return HttpNotFound();
            }
            return View(exam_Tabel);
        }

        // POST: Exam_Tabel/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Examid,Exam")] Exam_Tabel exam_Tabel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(exam_Tabel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(exam_Tabel);
        }

        // GET: Exam_Tabel/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Exam_Tabel exam_Tabel = db.Exam_Tabel.Find(id);
            if (exam_Tabel == null)
            {
                return HttpNotFound();
            }
            return View(exam_Tabel);
        }

        // POST: Exam_Tabel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Exam_Tabel exam_Tabel = db.Exam_Tabel.Find(id);
            db.Exam_Tabel.Remove(exam_Tabel);
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
