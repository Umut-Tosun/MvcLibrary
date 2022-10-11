using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcLibrary.Controllers
{
    public class AnnouncementController : Controller
    {
        DbLibraryEntities2 dbLibraryEntities1 = new DbLibraryEntities2();
        public ActionResult Index()
        {
            var announcements= dbLibraryEntities1.Tbl_Announcement.ToList();

            return View(announcements);
        }
        public ActionResult DeleteAnnouncement(Tbl_Announcement announcement)
        {
            var values = dbLibraryEntities1.Tbl_Announcement.Find(announcement.ID);
            if (values.Status == true) values.Status = false;
            else values.Status = true;
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult AddAnnouncement()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAnnouncement(Tbl_Announcement announcement)
        {
            announcement.Date = DateTime.Now;
            dbLibraryEntities1.Tbl_Announcement.Add(announcement);
            dbLibraryEntities1.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult GetAnnouncement(int id)
        {
            var values = dbLibraryEntities1.Tbl_Announcement.Find(id);
            return View("GetAnnouncement", values);
        }
        public ActionResult UpdateAnnouncement(Tbl_Announcement announcement)
        {
            var values = dbLibraryEntities1.Tbl_Announcement.Find(announcement.ID);

            values.Title = announcement.Title;
            values.Content = announcement.Content;
            values.Category = announcement.Category;

            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}