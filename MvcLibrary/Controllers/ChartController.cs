using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcLibrary.Models.Class;
namespace MvcLibrary.Controllers
{
    public class ChartController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VisiualizeBookResult()
        {
            return Json(list());
        }
        public List<Class2> list()
        {
            List<Class2> cs = new List<Class2>();
            cs.Add(new Class2()
            {
                PublisherName = "Güneş",
                BookCount = 7
            });
            cs.Add(new Class2()
            {
                PublisherName = "Dünya",
                BookCount = 5
            });
            cs.Add(new Class2()
            {
                PublisherName = "Mars",
                BookCount = 70
            });
            return cs;
        }
    }
}