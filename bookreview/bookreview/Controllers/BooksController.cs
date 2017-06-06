using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using bookreview.Models;
using bookreview.Models.BaseModels;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using iTextSharp.text.html;

namespace bookreview.Controllers
{
    public class BooksController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Books
        public ActionResult Index()
        {
            var x = View(db.Books.Include(b => b.Author).ToList());
            string y = x.ToString();
            return x;
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

        public ActionResult Pdf() {
            var books = db.Books.Include(b => b.Author).Include(b => b.CategoryList).Include(b => b.RateList);
            return View(books);
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

        public string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext,
                                                                         viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                                             ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
        [HttpGet]
        public void CreatePDF()
        {
            var books = db.Books;
            var doc = new Document();
            var output = new MemoryStream();
            PdfWriter.GetInstance(doc, output);
            doc.Open();
            var font = FontFactory.GetFont(BaseFont.TIMES_ROMAN, BaseFont.CP1257, 12);
            PdfPTable table = new PdfPTable(4);
            PdfPCell cell = new PdfPCell(new Phrase("Lista książek",font));
            cell.Colspan = 4;
            cell.HorizontalAlignment = 1;
            table.AddCell(cell);
            table.AddCell("Nazwa");
            table.AddCell("Autor");
            table.AddCell("Rok wydania");
            table.AddCell("Ocena");
            foreach (var book in db.Books.Include(b => b.RateList))
            {
                table.AddCell(book.Name);
                table.AddCell(book.Author.ToString());
                table.AddCell(book.ReleaseDate.ToString("yyyy"));
                table.AddCell(book.GetAverageOfRates().ToString());
            }
            doc.Add(table);
            doc.Close();

            Response.ContentType = "application/pdf";
            Response.BinaryWrite(output.ToArray());
        }
    }
}
