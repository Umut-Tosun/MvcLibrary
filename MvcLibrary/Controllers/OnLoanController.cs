using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    public class OnLoanController : Controller
    {
        DbLibraryEntities2 dbLibraryEntities1 = new DbLibraryEntities2();
        public ActionResult Index()
        {
            var values = dbLibraryEntities1.Transactions.Where(x => x.Status == false).ToList();
           
           
            return View(values);
        }
        [HttpGet]
        public ActionResult ToLend()
        {
            List<SelectListItem> books = (from i in dbLibraryEntities1.Tbl_Book.Where(x=>x.Status==true).ToList()
                                            select new SelectListItem
                                            {
                                                Text = i.Name,
                                                Value = i.ID.ToString()
                                            }).ToList();
            List<SelectListItem> members = (from i in dbLibraryEntities1.Tbl_Member.Where(x => x.Status == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.FirstName+" "+i.LastName,
                                                   Value = i.ID.ToString()
                                               }).ToList();
            List<SelectListItem> employs = (from i in dbLibraryEntities1.Tbl_Employee.Where(x => x.Status == true).ToList()
                                               select new SelectListItem
                                               {
                                                   Text = i.FirstName+" "+i.LastName,
                                                   Value = i.ID.ToString()
                                               }).ToList();
            ViewBag.books = books;
            ViewBag.members = members;
            ViewBag.employs = employs;

            return View();
        }
        [HttpPost]
        public ActionResult ToLend(Transactions transactions)
        {
            var member = dbLibraryEntities1.Tbl_Member.Where(k => k.ID == transactions.Tbl_Member.ID).FirstOrDefault();
            var book = dbLibraryEntities1.Tbl_Book.Where(k => k.ID == transactions.Tbl_Book.ID).FirstOrDefault();
            var employee = dbLibraryEntities1.Tbl_Employee.Where(k => k.ID == transactions.Tbl_Employee.ID).FirstOrDefault();

            transactions.Tbl_Member = member;
            transactions.Tbl_Book = book;
            transactions.Tbl_Employee = employee;
            transactions.Checkout_Date = DateTime.Now;
            book.Status = false;

            dbLibraryEntities1.Transactions.Add(transactions);
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("ToLend");
        }

        public ActionResult ReturnBook(int id)
        {
            var rtrnbook = dbLibraryEntities1.Transactions.Find(id);
            rtrnbook.Status = true;
            rtrnbook.Tbl_Book.Status=true;
            rtrnbook.MemberBookReturnDate = DateTime.Now;
            dbLibraryEntities1.SaveChanges();

            return RedirectToAction("Index");
        }

    }
}