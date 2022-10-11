using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcLibrary.Models.Entity;
namespace MvcLibrary.Controllers
{
    public class MessageController : Controller
    {
        DbLibraryEntities2 DbLibraryEntities1 = new DbLibraryEntities2();
        public ActionResult Index()
        {
            var memberMail = (string)Session["Mail"].ToString();
            var messages = DbLibraryEntities1.Messages.Where(x => x.ReceivingMember == memberMail).ToList();
            return View(messages);
        }
        public ActionResult Outbox()
        {
            var memberMail = (string)Session["Mail"].ToString();
            var messages = DbLibraryEntities1.Messages.Where(x => x.SendingMember == memberMail).ToList();
            return View(messages);
        }
        [HttpGet]
        public ActionResult SendMessage()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SendMessage(Messages message)
        {
            var memberMail = (string)Session["Mail"].ToString();
            message.SendingMember = memberMail;
            message.Date = DateTime.Now;
            if (memberMail == message.ReceivingMember)
            {
                ViewBag.Error = "Kendi kendinize mail atamazsınız";
                return View(message);
            }
            else
            {
                DbLibraryEntities1.Messages.Add(message);
                DbLibraryEntities1.SaveChanges();
                return RedirectToAction("Outbox", "Message");
            }

        }
        public PartialViewResult PartialSlider()
        {
            var memberMail = (string)Session["Mail"].ToString();
            ViewBag.mymessages = DbLibraryEntities1.Messages.Where(x => x.ReceivingMember == memberMail).Count();
            ViewBag.outbox = DbLibraryEntities1.Messages.Where(x => x.SendingMember == memberMail).Count();
            return PartialView();
        }
    }
}