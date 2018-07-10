using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ComicBookDataObjects;
using MVCPresentation.Models;
using ComicBookLogic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MVCPresentation.Controllers
{
    public class EmployeesController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private EmployeeManager _employeeManager = new EmployeeManager();

        /// <summary>
        /// Application DB context
        /// </summary>
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<ApplicationUser> UserManager { get; set; }

        // GET: Employees
        //public ActionResult Index()
        //{
        //    return View(_employeeManager.));
        //}

        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Employee employee = db.Employees.Find(id);
            var employee = _employeeManager.RetrieveEmployeeByID(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeID,FirstName,LastName,Address1,Address2,City,Zip,PhoneNumber,Email,Active")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeManager.CreateEmployee(employee.FirstName, employee.LastName, employee.PhoneNumber, employee.Zip, employee.Email);
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit()
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}

            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
            var user = UserManager.FindById(User.Identity.GetUserId());
            string email = user.Email;

            int? id = _employeeManager.RetrieveEmployeeByEmail(email).EmployeeID;

            Employee employee = _employeeManager.RetrieveEmployeeByID(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeID,FirstName,LastName,Address1,Address2,City,Zip,PhoneNumber,Email,Active")] Employee newEmployee)
        {
            if (ModelState.IsValid)
            {
                var oldEmployee = _employeeManager.RetrieveEmployeeByID(newEmployee.EmployeeID);
                if (_employeeManager.UpdateEmployee(oldEmployee, newEmployee.FirstName, newEmployee.LastName, newEmployee.PhoneNumber,
                    newEmployee.Zip, newEmployee.Email, newEmployee.Active) == true)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                }
            }
            else
            {
                return View(Details(newEmployee.EmployeeID));
            }
        }

        // GET: Employees/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(employee);
        //}

        //// POST: Employees/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Employee employee = db.Employees.Find(id);
        //    db.Employees.Remove(employee);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
