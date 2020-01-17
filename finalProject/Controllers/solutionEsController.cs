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
    public class solutionEsController : Controller
    {
        private IdentityDBEntities15 db = new IdentityDBEntities15();

        // GET: solutionEs
        public ActionResult Index()
        {
            return View(db.solutionEs.ToList());
        }

        // GET: solutionEs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solutionE solutionE = db.solutionEs.Find(id);
            if (solutionE == null)
            {
                return HttpNotFound();
            }
            return View(solutionE);
        }

        // GET: solutionEs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: solutionEs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "studentID,ExamNumber,solutionExam")] solutionE solutionE)
        {
            if (ModelState.IsValid)
            {
                db.solutionEs.Add(solutionE);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(solutionE);
        }

        // GET: solutionEs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solutionE solutionE = db.solutionEs.Find(id);
            if (solutionE == null)
            {
                return HttpNotFound();
            }
            return View(solutionE);
        }

        // POST: solutionEs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "studentID,ExamNumber,solutionExam")] solutionE solutionE)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solutionE).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(solutionE);
        }

        // GET: solutionEs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            solutionE solutionE = db.solutionEs.Find(id);
            if (solutionE == null)
            {
                return HttpNotFound();
            }
            return View(solutionE);
        }

        // POST: solutionEs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            solutionE solutionE = db.solutionEs.Find(id);
            db.solutionEs.Remove(solutionE);
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
