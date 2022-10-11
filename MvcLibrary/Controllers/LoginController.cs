using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcLibrary.Models.Entity;

namespace MvcLibrary.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        // GET: Login

        DbLibraryEntities2 dbLibraryEntities1 = new DbLibraryEntities2();

        [HttpGet]
        public ActionResult SignOn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignOn(Tbl_Member member)
        {
            ViewBag.infoError = " ";
            var info = dbLibraryEntities1.Tbl_Member.FirstOrDefault(x =>
            x.Email == member.Email && x.Password == member.Password);

            if (info != null)
            {
                FormsAuthentication.SetAuthCookie(member.Email, false);
                Session["Mail"] = info.Email.ToString();
                //TempData["ID"] = info.ID.ToString();
                //TempData["FirstName"] = info.FirstName.ToString();
                //TempData["LastName"] = info.LastName.ToString();
                //TempData["UserName"] = info.UserName.ToString();
                //TempData["School"] = info.School.ToString();
                return RedirectToAction("Index", "MemberDashboard");
            }
            else
            {
                ViewBag.infoError = "Kullanıcı adı ya da şifreniz hatalı lütfen tekrar deneyiniz.";
                return View();
            }

        }

        [HttpGet]
        public ActionResult SignIn()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SignIn(Tbl_Member member)
        {
            dbLibraryEntities1.Tbl_Member.Add(member);
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("SignOn","Login");
        }
    }
}