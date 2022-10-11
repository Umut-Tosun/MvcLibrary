using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcLibrary.Models.Entity;

namespace MvcLibrary.Controllers
{
    public class StatisticsController : Controller
    {
        DbLibraryEntities2 dbLibraryEntities1 = new DbLibraryEntities2();
        public ActionResult Index()
        {
            ViewBag.TotalMoney = dbLibraryEntities1.Tbl_Punishment.Sum(x => x.PunishmentMoney).Value;
            ViewBag.TotalBook = dbLibraryEntities1.Tbl_Book.Count();
            ViewBag.TotalMember = dbLibraryEntities1.Tbl_Member.Count();
            ViewBag.TotalDisableBook = dbLibraryEntities1.Tbl_Book.Where(x => x.Status == false).Count();
            return View();
        }
        public ActionResult WeatherForecast()
        {
            return View();
        }
        public ActionResult WeatherWidget()
        {
            return View();
        }
        public ActionResult Gallery()
        {
            return View();
        }
        public ActionResult UploadImage(HttpPostedFileBase File)
        {
            if (File.ContentLength > 0)
            {
                string filepath = Path.Combine(Server.MapPath("~/Gallery/"), Path.GetFileName(File.FileName));
                File.SaveAs(filepath);
            }
            return RedirectToAction("Gallery");
        }
        public ActionResult LinqWidget()
        {
            ViewBag.TotalBook = dbLibraryEntities1.Tbl_Book.Count();
            ViewBag.TotalMember= dbLibraryEntities1.Tbl_Member.Count();
            ViewBag.TotalMoney = dbLibraryEntities1.Tbl_Punishment.Sum(x => x.PunishmentMoney).Value;
            ViewBag.TotalDisableBook = dbLibraryEntities1.Tbl_Book.Where(x => x.Status == false).Count();
            ViewBag.TotalCategory = dbLibraryEntities1.Tbl_Categories.Count();
            ViewBag.WriterName = dbLibraryEntities1.EnFazlaKitapYazar1().FirstOrDefault();
            ViewBag.TotalContact = dbLibraryEntities1.Tbl_Contact.Count();
            ViewBag.BestPublisher = dbLibraryEntities1.BestPublisher().FirstOrDefault();
            ViewBag.TotalBookGiveToday = dbLibraryEntities1.Transactions.Where(x => x.Checkout_Date == DateTime.Now).ToList().Count;
            ViewBag.BestMember = dbLibraryEntities1.BestMember().FirstOrDefault();
            ViewBag.BestEmployee = dbLibraryEntities1.BestEmployee().FirstOrDefault();
            ViewBag.BestBook = dbLibraryEntities1.BestBook().FirstOrDefault();

            return View();
        }
    }
}