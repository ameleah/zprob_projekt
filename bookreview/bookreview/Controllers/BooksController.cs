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
            var pHTML = RenderRazorViewToString("Pdf", books);
            
            MemoryStream ms = new MemoryStream();
            
            TextReader txtReader = new StringReader(pHTML);

            // 1: create object of a itextsharp document class
            Document doc = new Document(PageSize.A4, 25, 25, 25, 25);

            // 2: we create a itextsharp pdfwriter that listens to the document and directs a XML-stream to a file
            PdfWriter oPdfWriter = PdfWriter.GetInstance(doc, new FileStream(Server.MapPath("~/raport.pdf"), FileMode.Create));


            // 3: we create a worker parse the document
            HTMLWorker htmlWorker = new HTMLWorker(doc);

            // 4: we open document and start the worker on the document
            doc.Open();

            FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1257, 18, Font.BOLD);
            // step 4.1: register a unicode font and assign it an allias
          //  FontFactory.Register("C:\\Windows\\Fonts\\Arial.TTF", "arial unicode ms");
            var font = FontFactory.GetFont(BaseFont.COURIER, BaseFont.CP1257, 18, Font.BOLD);
            // step 4.2: create a style sheet and set the encoding to Identity-H
            StyleSheet ST = new StyleSheet();
            ST.LoadTagStyle(HtmlTags.BODY, HtmlTags.FACE, "Arial Unicode MS");

            // step 4.3: assign the style sheet to the html parser
            htmlWorker.SetStyleSheet(ST);


            htmlWorker.StartDocument();

            // 5: parse the html into the document
            htmlWorker.Parse(txtReader);

            // 6: close the document and the worker
            htmlWorker.EndDocument();
            htmlWorker.Close();
            doc.Close();

          //  System.IO.File.WriteAllBytes(Server.MapPath("~/raport.pdf"), ms.GetBuffer());            
        }
    }
}
