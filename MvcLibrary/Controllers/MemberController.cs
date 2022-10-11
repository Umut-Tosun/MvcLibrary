using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcLibrary.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcLibrary.Controllers
{
    public class MemberController : Controller
    {
        DbLibraryEntities2 dbLibraryEntities1 = new DbLibraryEntities2();

        public ActionResult Index()
        {
            //var members = from k in dbLibraryEntities1.Tbl_Member select k;
            //if (!string.IsNullOrEmpty(memberName))
            //{
            //    members = members.Where(m => m.FirstName.Contains(memberName));
            //}

            var members = dbLibraryEntities1.Tbl_Member.ToList();
          
            return View(members);
        }
        [HttpGet]
        public ActionResult AddMember()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult AddMember(Tbl_Member member)
        {
            if (!ModelState.IsValid)
            {
                return View("AddMember");
            }

            dbLibraryEntities1.Tbl_Member.Add(member);
            dbLibraryEntities1.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DeleteMember(Tbl_Member member)
        {
            var values = dbLibraryEntities1.Tbl_Member.Find(member.ID);
            if (values.Status == true) values.Status = false;
            else values.Status = true;
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetMember(Tbl_Member member)
        {
            var values = dbLibraryEntities1.Tbl_Member.Find(member.ID);
            return View("GetMember", values);
        }
        public ActionResult GetMemberDetails(int id)
        {
            var values = dbLibraryEntities1.Transactions.Where(x => x.Tbl_Member.ID == id).ToList();
            ViewBag.memberFullName = dbLibraryEntities1.Tbl_Member.Where(x => x.ID == id).Select(x => x.FirstName + " " + x.LastName).FirstOrDefault();
            return View("GetMemberDetails", values);
        }
        public ActionResult UpdateMember(Tbl_Member member)
        {
            var values = dbLibraryEntities1.Tbl_Member.Find(member.ID);

            values.FirstName = member.FirstName;
            values.LastName = member.LastName;
            values.Email = member.Email;
            values.PhoneNumber = member.PhoneNumber;
            values.School = member.School;
            values.ImageUrl = member.ImageUrl;
            values.UserName = member.UserName;
            values.Password = member.Password;
            
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}