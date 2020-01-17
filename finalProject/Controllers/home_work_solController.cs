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
    public class home_work_solController : Controller
    {
        private IdentityDBEntities13 db = new IdentityDBEntities13();

        // GET: home_work_sol
        public ActionResult Index()
        {
            return View(db.home_work_sol.ToList());
        }

        // GET: home_work_sol/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            home_work_sol home_work_sol = db.home_work_sol.Find(id);
            if (home_work_sol == null)
            {
                return HttpNotFound();
            }
            return View(home_work_sol);
        }

        // GET: home_work_sol/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: home_work_sol/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "studentId,home_work_number,solution")] home_work_sol home_work_sol)
        {
            if (ModelState.IsValid)
            {
                db.home_work_sol.Add(home_work_sol);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(home_work_sol);
        }

        // GET: home_work_sol/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            home_work_sol home_work_sol = db.home_work_sol.Find(id);
            if (home_work_sol == null)
            {
                return HttpNotFound();
            }
            return View(home_work_sol);
        }

        // POST: home_work_sol/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "studentId,home_work_number,solution")] home_work_sol home_work_sol)
        {
            if (ModelState.IsValid)
            {
                db.Entry(home_work_sol).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(home_work_sol);
        }

        // GET: home_work_sol/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            home_work_sol home_work_sol = db.home_work_sol.Find(id);
            if (home_work_sol == null)
            {
                return HttpNotFound();
            }
            return View(home_work_sol);
        }

        // POST: home_work_sol/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            home_work_sol home_work_sol = db.home_work_sol.Find(id);
            db.home_work_sol.Remove(home_work_sol);
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
