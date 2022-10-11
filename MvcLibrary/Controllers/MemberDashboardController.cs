using MvcLibrary.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MvcLibrary.Controllers
{
    [Authorize]
    public class MemberDashboardController : Controller
    {
        // GET: MemberDashboard
        DbLibraryEntities2 dbLibraryEntities1 = new DbLibraryEntities2();
      
        public ActionResult Index()
        {
            var memberMail = (string)Session["Mail"];
            //var values = dbLibraryEntities1.Tbl_Member.FirstOrDefault(x => x.Email == memberMail);
            var values = dbLibraryEntities1.Tbl_Announcement.ToList();

            ViewBag.memberFullName = dbLibraryEntities1.Tbl_Member.Where(x=>x.Email==memberMail).Select(x=>x.FirstName+" "+x.LastName).FirstOrDefault();
            ViewBag.memberImageUrl = dbLibraryEntities1.Tbl_Member.Where(x=>x.Email==memberMail).Select(x=>x.ImageUrl).FirstOrDefault();
            ViewBag.memberEmail = dbLibraryEntities1.Tbl_Member.Where(x=>x.Email==memberMail).Select(x=>x.Email).FirstOrDefault();
            ViewBag.SchoolName = dbLibraryEntities1.Tbl_Member.Where(x=>x.Email==memberMail).Select(x=>x.School).FirstOrDefault();
            ViewBag.PhoneNumber = dbLibraryEntities1.Tbl_Member.Where(x=>x.Email==memberMail).Select(x=>x.PhoneNumber).FirstOrDefault();
            ViewBag.Mail = dbLibraryEntities1.Tbl_Member.Where(x=>x.Email==memberMail).Select(x=>x.Email).FirstOrDefault();
            ViewBag.Status = dbLibraryEntities1.Tbl_Member.Where(x=>x.Email==memberMail).Select(x=>x.Status).FirstOrDefault().ToString();
            var memberID = dbLibraryEntities1.Tbl_Member.Where(x => x.Email == memberMail).Select(x=>x.ID).FirstOrDefault();
            ViewBag.TotalCountOfMemberGetBook = dbLibraryEntities1.Transactions.Where(x => x.Member_Id == memberID).Count();
            ViewBag.TotalPunishmentMember = dbLibraryEntities1.Tbl_Punishment.Where(x => x.Member_Id == memberID).Sum(x => x.PunishmentMoney).GetValueOrDefault(0);
            ViewBag.TotalMessage = dbLibraryEntities1.Messages.Where(x => x.ReceivingMember == memberMail).Count();

            return View(values);
        }
      
        public ActionResult Index2(Tbl_Member member)
        {
            var user = (string)Session["Mail"];
            var mem = dbLibraryEntities1.Tbl_Member.FirstOrDefault(x => x.Email == user);
            if (member.Password!=null)
                mem.Password = member.Password;

            mem.FirstName = member.FirstName;
            mem.LastName = member.LastName;
            mem.School = member.School;
            mem.ImageUrl = member.ImageUrl;
            dbLibraryEntities1.SaveChanges();
         
            return RedirectToAction("Index");
        }

        public ActionResult MyBooks()
        {
            var user = (string)Session["Mail"];
            var memberId= dbLibraryEntities1.Tbl_Member.Where(x => x.Email == user).Select(x=>x.ID).FirstOrDefault();
            var values = dbLibraryEntities1.Transactions.Where(x=> x.Member_Id == memberId).ToList();
            return View(values);
        }
        public ActionResult Announcements()
        {    
            var values = dbLibraryEntities1.Tbl_Announcement.Where(x=>x.Status==true).ToList();
            return View(values);
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "ShowCase");
        }
        public PartialViewResult PartialAnnouncement()
        {
            var announcements = dbLibraryEntities1.Tbl_Announcement.ToList();

            return PartialView("PartialAnnouncement", announcements);
        }
        public PartialViewResult PartialSettings()
        {
            var memberMail = (string)Session["Mail"];
            var memberID = dbLibraryEntities1.Tbl_Member.Where(x => x.Email == memberMail).Select(x => x.ID).FirstOrDefault();
            var getMember = dbLibraryEntities1.Tbl_Member.Find(memberID);
            return PartialView("PartialSettings",getMember);
        }
    }
}