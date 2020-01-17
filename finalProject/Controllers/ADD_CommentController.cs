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
    public class ADD_CommentController : Controller
    {
        private IdentityDBEntities7 db = new IdentityDBEntities7();

        // GET: ADD_Comment
        public ActionResult Index(string searchString)
        {
            var Comment = from s in db.ADD_Comment
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                Comment = Comment.Where(s => s.StudentID.Contains(searchString));
            }
            return View(Comment.ToList());
        }

        // GET: ADD_Comment/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADD_Comment aDD_Comment = db.ADD_Comment.Find(id);
            if (aDD_Comment == null)
            {
                return HttpNotFound();
            }
            return View(aDD_Comment);
        }

        // GET: ADD_Comment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ADD_Comment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,Comment,Date")] ADD_Comment aDD_Comment)
        {
            if (ModelState.IsValid)
            {
                db.ADD_Comment.Add(aDD_Comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(aDD_Comment);
        }

        // GET: ADD_Comment/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADD_Comment aDD_Comment = db.ADD_Comment.Find(id);
            if (aDD_Comment == null)
            {
                return HttpNotFound();
            }
            return View(aDD_Comment);
        }

        // POST: ADD_Comment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,Comment,Date")] ADD_Comment aDD_Comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(aDD_Comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aDD_Comment);
        }

        // GET: ADD_Comment/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ADD_Comment aDD_Comment = db.ADD_Comment.Find(id);
            if (aDD_Comment == null)
            {
                return HttpNotFound();
            }
            return View(aDD_Comment);
        }

        // POST: ADD_Comment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ADD_Comment aDD_Comment = db.ADD_Comment.Find(id);
            db.ADD_Comment.Remove(aDD_Comment);
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
