using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using bookreview.Models;
using bookreview.Models.BaseModels;
using Microsoft.AspNet.Identity;
using System.Web.Routing;

namespace bookreview.Controllers
{
    public class RatesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Rates
        public ActionResult Index()
        {
            return View(db.Rates.ToList());
        }

        // GET: Rates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // GET: Rates/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Rates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string EntityType, string EntityId, string Value)
        {
            if (EntityType != null && EntityId != null && Value != null)
            {
                bool entityType = EntityType.ToLower() == "true";
                int entityId = Int32.Parse(EntityId);
                int val = Int32.Parse(Value);
                Rate rate;
                if (entityType)
                {
                    Book book = db.Books.Find(entityId);
                    rate = new Rate(db.Users.Find(User.Identity.GetUserId()), entityType, val, book);
                }
                else
                {
                    Author author = db.Authors.Find(entityId);
                    rate = new Rate(db.Users.Find(User.Identity.GetUserId()), entityType, val, author);
                }
                db.Rates.Add(rate);
                db.SaveChanges();
                return RedirectToAction("Details", entityType ? "Books" : "Authors", new RouteValueDictionary { { "Id", entityId } });
            }

            return View();
        }

        // GET: Rates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // POST: Rates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,EntityType,Value,CreatedAt,UpdatedAt")] Rate rate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(rate);
        }

        // GET: Rates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rate rate = db.Rates.Find(id);
            if (rate == null)
            {
                return HttpNotFound();
            }
            return View(rate);
        }

        // POST: Rates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Rate rate = db.Rates.Find(id);
            db.Rates.Remove(rate);
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
