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
    public class BooksRequestsController : Controller
    {
        private IdentityDBEntities1 db = new IdentityDBEntities1();

        // GET: BooksRequests
        public ActionResult Index(string searchString)
        {
           
            var requsets = from s in db.BooksRequests
                           select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                requsets = requsets.Where(s => s.bookname.Contains(searchString)
                                       || s.author_.Contains(searchString));
            }

            return View(requsets.ToList());


            //return View(db.BooksRequests.ToList());
        }

        // GET: BooksRequests/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksRequest booksRequest = db.BooksRequests.Find(id);
            if (booksRequest == null)
            {
                return HttpNotFound();
            }
            return View(booksRequest);
        }

        // GET: BooksRequests/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BooksRequests/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bookname,author_")] BooksRequest booksRequest)
        {
            if (ModelState.IsValid)
            {
                db.BooksRequests.Add(booksRequest);
                db.SaveChanges();
                return RedirectToAction("Create");
            }

            return View(booksRequest);
        }

        // GET: BooksRequests/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksRequest booksRequest = db.BooksRequests.Find(id);
            if (booksRequest == null)
            {
                return HttpNotFound();
            }
            return View(booksRequest);
        }

        // POST: BooksRequests/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bookname,author_")] BooksRequest booksRequest)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booksRequest).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(booksRequest);
        }

        // GET: BooksRequests/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BooksRequest booksRequest = db.BooksRequests.Find(id);
            if (booksRequest == null)
            {
                return HttpNotFound();
            }
            return View(booksRequest);
        }

        // POST: BooksRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            BooksRequest booksRequest = db.BooksRequests.Find(id);
            db.BooksRequests.Remove(booksRequest);
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
