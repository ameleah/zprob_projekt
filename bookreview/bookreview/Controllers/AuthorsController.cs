using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using bookreview.Models;
using bookreview.Models.BaseModels;

namespace bookreview.Controllers
{
    public class AuthorsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Authors
        public ActionResult Index()
        {
            try
            {
                return View(db.Authors.ToList());
            }
            catch(Exception e)
            {
                throw e;
            }
        }

        // GET: Authors/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Author author = db.Authors.Include(a => a.RateList).ToList().Find(a => a.Id == id);
            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // GET: Authors/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string FirstName, string LastName, string BirthDate, string Bio)
        {
            if (FirstName != null && LastName != null && BirthDate != null && Bio != null)
            {
                string[] dateString = BirthDate.Split('/');
                int[] dateInt = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    dateInt[i] = Int32.Parse(dateString[i]);
                }
                DateTime birthDate = new DateTime(dateInt[2], dateInt[0], dateInt[1]);
                Author author = new Author(FirstName, LastName, birthDate, Bio);
                db.Authors.Add(author);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
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
