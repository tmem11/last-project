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
    public class Student_GredesController : Controller
    {
        private IdentityDBEntities8 db = new IdentityDBEntities8();

        // GET: Student_Gredes
        public ActionResult Index(string Grade)
        {
            var StudenrG = from s in db.Student_Gredes
                           select s;

            if (!String.IsNullOrEmpty(Grade))
            {
                StudenrG = StudenrG.Where(s => s.StudentID.Contains(Grade));
            }

            return View(StudenrG.ToList());
        }

        // GET: Student_Gredes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Gredes student_Gredes = db.Student_Gredes.Find(id);
            if (student_Gredes == null)
            {
                return HttpNotFound();
            }
            return View(student_Gredes);
        }

        // GET: Student_Gredes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Student_Gredes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentID,Grades")] Student_Gredes student_Gredes)
        {
            if (ModelState.IsValid)
            {
                db.Student_Gredes.Add(student_Gredes);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(student_Gredes);
        }

        // GET: Student_Gredes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Gredes student_Gredes = db.Student_Gredes.Find(id);
            if (student_Gredes == null)
            {
                return HttpNotFound();
            }
            return View(student_Gredes);
        }

        // POST: Student_Gredes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentID,Grades")] Student_Gredes student_Gredes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student_Gredes).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student_Gredes);
        }

        // GET: Student_Gredes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student_Gredes student_Gredes = db.Student_Gredes.Find(id);
            if (student_Gredes == null)
            {
                return HttpNotFound();
            }
            return View(student_Gredes);
        }

        // POST: Student_Gredes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Student_Gredes student_Gredes = db.Student_Gredes.Find(id);
            db.Student_Gredes.Remove(student_Gredes);
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
