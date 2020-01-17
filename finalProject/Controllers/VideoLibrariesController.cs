using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using finalProject.Models;

namespace finalProject.Controllers
{
    public class VideoLibrariesController : Controller
    {
        private IdentityDBEntities db = new IdentityDBEntities();

        // GET: VideoLibraries
        [Authorize(Roles = "Students")]
        public ActionResult StudentsPages2()
        {
            return View();
        }
        public ActionResult watchvideo()
        {
            return View(db.VideoLibraries.ToList());
        }
        public ActionResult Index()
        {
            return View(db.VideoLibraries.ToList());
        }

        // GET: VideoLibraries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoLibrary videoLibrary = db.VideoLibraries.Find(id);
            if (videoLibrary == null)
            {
                return HttpNotFound();
            }
            return View(videoLibrary);
        }

        // GET: VideoLibraries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VideoLibraries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "videoNumber,videoLink,Title")] VideoLibrary videoLibrary)
        {
            if (ModelState.IsValid)
            {
                db.VideoLibraries.Add(videoLibrary);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(videoLibrary);
        }

        // GET: VideoLibraries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoLibrary videoLibrary = db.VideoLibraries.Find(id);
            if (videoLibrary == null)
            {
                return HttpNotFound();
            }
            return View(videoLibrary);
        }

        // POST: VideoLibraries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "videoNumber,videoLink,Title")] VideoLibrary videoLibrary)
        {
            if (ModelState.IsValid)
            {
                db.Entry(videoLibrary).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(videoLibrary);
        }

        // GET: VideoLibraries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            VideoLibrary videoLibrary = db.VideoLibraries.Find(id);
            if (videoLibrary == null)
            {
                return HttpNotFound();
            }
            return View(videoLibrary);
        }

        // POST: VideoLibraries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            VideoLibrary videoLibrary = db.VideoLibraries.Find(id);
            db.VideoLibraries.Remove(videoLibrary);
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
