using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using MvcLibrary.Models.Entity;

namespace MvcLibrary.Controllers
{
    
    public class AdminController : Controller
    {
        DbLibraryEntities2 dbLibraryEntities1 = new DbLibraryEntities2();

        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Tbl_Admin admin)
        {
            var info = dbLibraryEntities1.Tbl_Admin.FirstOrDefault(x => x.UserName == admin.UserName && x.Password == admin.Password);
            if(info!=null)
            {
                FormsAuthentication.SetAuthCookie(info.UserName,false);
                Session["UserName"] = info.UserName.ToString();
                return RedirectToAction("Index","Category");
            }
            else
            {
                return View();
            }
           
        }
       public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "ShowCase");
        }
        public ActionResult Settings2()
        {
            var users = dbLibraryEntities1.Tbl_Admin.Where(x=>x.Status==true).ToList();
            return View(users);
        }
        [HttpPost]
        public ActionResult AddAdmin(Tbl_Admin admin)
        {
            admin.Status = true;
            dbLibraryEntities1.Tbl_Admin.Add(admin);
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Settings2");
        }
        public ActionResult DeleteAdmin(Tbl_Admin admin)
        {
            var values = dbLibraryEntities1.Tbl_Admin.Find(admin.ID);
            values.Status = false;
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Settings2");
        }
        public ActionResult GetAdmin(Tbl_Admin Admin)
        {
            var values = dbLibraryEntities1.Tbl_Admin.Find(Admin.ID);
            return View("GetAdmin", values);
        }
        public ActionResult UpdateAdmin(Tbl_Admin Admin)
        {
            var values = dbLibraryEntities1.Tbl_Admin.Find(Admin.ID);
            values.UserName = Admin.UserName;
            values.Password = Admin.Password;
            values.Role = Admin.Role;
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Settings2");
        }
    }
}