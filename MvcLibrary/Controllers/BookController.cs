using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using MvcLibrary.Models.Entity;

namespace MvcLibrary.Controllers
{
    
    public class BookController : Controller
    {
        DbLibraryEntities2 dbLibraryEntities1 = new DbLibraryEntities2();
        public ActionResult Index(string bookName)
        {
            //var values = dbLibraryEntities1.Tbl_Book.ToList();
            var books = from k in dbLibraryEntities1.Tbl_Book select k;
            if (!string.IsNullOrEmpty(bookName))
            {
                books = books.Where(m => m.Name.Contains(bookName));
            }
            return View(books.ToList());
        }
        [HttpGet]
        public ActionResult AddBook()
        {
            List<SelectListItem> writers = (from i in dbLibraryEntities1.Tbl_Writer.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.FirstName + " " + i.LastName,
                                                Value = i.ID.ToString()
                                            }).ToList();
            List<SelectListItem> categories = (from i in dbLibraryEntities1.Tbl_Categories.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.Name,
                                                   Value = i.ID.ToString()
                                               }).ToList();
            List<SelectListItem> publishers = (from i in dbLibraryEntities1.Tbl_Publisher.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.Name,
                                                   Value = i.ID.ToString()
                                               }).ToList();
            ViewBag.writers = writers;
            ViewBag.categories = categories;
            ViewBag.publishers = publishers;

            return View();
        }
        [HttpPost]
        public ActionResult AddBook(Tbl_Book book)
        {
            var category = dbLibraryEntities1.Tbl_Categories.Where(k => k.ID == book.Tbl_Categories.ID).FirstOrDefault();
            var writer = dbLibraryEntities1.Tbl_Writer.Where(k => k.ID == book.Tbl_Writer.ID).FirstOrDefault();
            var publisher = dbLibraryEntities1.Tbl_Publisher.Where(k => k.ID == book.Tbl_Publisher.ID).FirstOrDefault();
           
            book.Tbl_Categories= category;
            book.Tbl_Writer = writer;
            book.Tbl_Publisher = publisher;
            book.Status = true;

            if (!ModelState.IsValid)
            {
                return View("AddBook",book);
            }

            dbLibraryEntities1.Tbl_Book.Add(book);
            dbLibraryEntities1.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DeleteBook(Tbl_Book book)
        {
            var values = dbLibraryEntities1.Tbl_Book.Find(book.ID);
            if (values.Status == true) values.Status = false;
            else values.Status = true;
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetBook(Tbl_Book book)
        {
            List<SelectListItem> writers = (from i in dbLibraryEntities1.Tbl_Writer.ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.FirstName + " " + i.LastName,
                                                Value = i.ID.ToString()
                                            }).ToList();
            List<SelectListItem> categories = (from i in dbLibraryEntities1.Tbl_Categories.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.Name,
                                                   Value = i.ID.ToString()
                                               }).ToList();
            List<SelectListItem> publishers = (from i in dbLibraryEntities1.Tbl_Publisher.ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.Name,
                                                   Value = i.ID.ToString()
                                               }).ToList();
            ViewBag.writers = writers;
            ViewBag.categories = categories;
            ViewBag.publishers = publishers;

            var values = dbLibraryEntities1.Tbl_Book.Find(book.ID);
            return View("GetBook", values);
        }
        public ActionResult UpdateBook(Tbl_Book book)
        {
            var values = dbLibraryEntities1.Tbl_Book.Find(book.ID);

            var category = dbLibraryEntities1.Tbl_Categories.Where(k => k.ID == book.Tbl_Categories.ID).FirstOrDefault();
            var writer = dbLibraryEntities1.Tbl_Writer.Where(k => k.ID == book.Tbl_Writer.ID).FirstOrDefault();
            var publisher = dbLibraryEntities1.Tbl_Publisher.Where(k => k.ID == book.Tbl_Publisher.ID).FirstOrDefault();
            values.Name = book.Name;
            values.PageCount = book.PageCount;
            values.PublicationYear = book.PublicationYear;
            values.Tbl_Categories = category;
            values.Tbl_Writer = writer;
            values.Tbl_Publisher = publisher;


            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}