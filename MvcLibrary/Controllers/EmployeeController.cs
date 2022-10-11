using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcLibrary.Models.Entity;

namespace MvcLibrary.Controllers
{
    public class EmployeeController : Controller
    {
        DbLibraryEntities2 dbLibraryEntities1 = new DbLibraryEntities2();
        public ActionResult Index(string employeeName)
        {
            var employee = from k in dbLibraryEntities1.Tbl_Employee select k;
            if (!string.IsNullOrEmpty(employeeName))
            {
                employee = employee.Where(m => m.FirstName.Contains(employeeName));
            }
            return View(employee.ToList());
        }
        [HttpGet]
        public ActionResult AddEmployee()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEmployee(Tbl_Employee employee)
        {
            if(!ModelState.IsValid)
            {
                return View("AddEmployee");
            }

            dbLibraryEntities1.Tbl_Employee.Add(employee);
            dbLibraryEntities1.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult DeleteEmployee(Tbl_Employee employee)
        {
            var values = dbLibraryEntities1.Tbl_Employee.Find(employee.ID);
            if (values.Status == true) values.Status = false;
            else values.Status = true;
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult GetEmployee(Tbl_Employee employee)
        {
            var values = dbLibraryEntities1.Tbl_Employee.Find(employee.ID);
            return View("GetEmployee", values);
        }
        public ActionResult UpdateEmployee(Tbl_Employee employee)
        {
            var values = dbLibraryEntities1.Tbl_Employee.Find(employee.ID);

            values.FirstName = employee.FirstName;
            values.LastName = employee.LastName;
            values.Email = employee.Email;
            values.MobilePhone = employee.MobilePhone;

            if (!ModelState.IsValid)
            {
                return View("GetEmployee");
            }
            dbLibraryEntities1.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}