using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using bookreview.Models;
using bookreview.Models.BaseModels;

namespace bookreview.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index()
        {
            return View(db.Books.Include(b => b.Author).ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var books = db.Books.Include(b => b.Author).Include(b => b.CategoryList).Include(b => b.RateList);
            Book book = books.Where(b => b.Id == id).FirstOrDefault();
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            var authors = db.Authors.ToList();
            var categories = db.Categories.ToList();
            SelectList authorsList = new SelectList((from a in authors select new { Id = a.Id, FullName = a.LastName + ", " + a.FirstName }), "Id", "FullName");
            MultiSelectList categoryList = new MultiSelectList(categories, "Id", "Name");
            ViewBag.Authors = authorsList;
            ViewBag.Categories = categoryList;
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Name, string Author_Id, string ReleaseDate, string Description, string[] CategoryList)
        {
            var authors = db.Authors;
            var categories = db.Categories;
            if (Name != null && Author_Id != null && ReleaseDate != null)
            {
                Author author = authors.Find(Int32.Parse(Author_Id));
                string[] dateString = ReleaseDate.Split('/');
                int[] dateInt = new int[3];
                for (int i = 0; i < 3; i++)
                {
                    dateInt[i] = Int32.Parse(dateString[i]);
                }
                DateTime releaseDate = new DateTime(dateInt[2], dateInt[0], dateInt[1]);
                Book book = new Book(Name, author, releaseDate, Description);
                foreach (string idc in CategoryList)
                {
                    Category cat = categories.Find(Int32.Parse(idc));
                    book.AddCategory(cat);

                }
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SelectList authorsList = new SelectList((from a in authors.ToList() select new { Id = a.Id, FullName = a.LastName + ", " + a.FirstName }), "Id", "FullName");
            MultiSelectList categoryList = new MultiSelectList(categories.ToList(), "Id", "Name");
            ViewBag.Authors = authorsList;
            ViewBag.Categories = categoryList;
            ViewBag.Authors = authorsList;

            return View();
        }
    }
}
