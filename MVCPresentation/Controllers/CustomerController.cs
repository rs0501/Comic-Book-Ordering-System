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

namespace MVCPresentation.Controllers
{
    public class CustomerController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private CustomerManager _customerManager = new CustomerManager();

        // GET: Customer
        public ActionResult Index()
        {
            return View(_customerManager.CurrentCustomersList());
        }

        // GET: Customer/Deactivated
        public ActionResult Deactivated()
        {
            return View(_customerManager.DeactivatedCustomersList());
        }

        // GET: Customer/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var customer = _customerManager.RetrieveCustomerByID(id);

            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customer/PullList
        public ActionResult PullList(int? id)
        {
            ComicManager _comicManager = new ComicManager();

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var pullList = _comicManager.PullListForCustomer(id);
            Customer customer = _customerManager.RetrieveCustomerByID(id);
            string fName = customer.FirstName;
            string lName = customer.LastName;
            ViewBag.Message = "Pull list for " + fName + " " + lName;

            if (null == pullList)
            {
                ViewBag.Message = "Customer has no pull list!";
            }

            return View(pullList);
        }

        // GET: Customer/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Customer/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Zip,PhoneNumber,Email,Active")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                _customerManager.CreateCustomerAccount(customer.FirstName, customer.LastName, customer.PhoneNumber, customer.Zip, customer.Email);
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // GET: Customer/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var customer = _customerManager.RetrieveCustomerByID(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customer/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Zip,PhoneNumber,Email,Active")] Customer newCustomer)
        {
            if (ModelState.IsValid)
            {
                var oldCustomer = _customerManager.RetrieveCustomerByID(newCustomer.CustomerID);
                if (_customerManager.UpdateCustomer(oldCustomer, newCustomer.FirstName, newCustomer.LastName, newCustomer.PhoneNumber, 
                    newCustomer.Zip, newCustomer.Email, newCustomer.Active) == true)
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
                return View(newCustomer);
            }
        }

        // GET: Comics/Remove/5
        public ActionResult RemoveFromPull(int? id)
        {
            ComicManager _comicManager = new ComicManager();
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var comic = _comicManager.RetrieveAvailableComics(true).Find(c => c.ComicID == id);
            if (comic == null)
            {
                return HttpNotFound();
            }
            return View(comic);
        }

        //// POST: Comics/Remove/5
        //[HttpPost]
        //[ActionName("RemoveFromPull")]
        //[ValidateAntiForgeryToken]
        //public ActionResult RemoveFromPullConfirmed(int? id)
        //{
            //if (ModelState.IsValid)
            //{
            //this.ApplicationDbContext = new ApplicationDbContext();
            //this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
            //var user = UserManager.FindById(User.Identity.GetUserId());
            //string email = user.Email;
            
            //var customerId = _customerManager.RetrieveCustomerIdByEmail(email);
            //var comicId = id;

            //if (_comicManager.RemoveComicFromPullList(comicId, customerId) == true)
            //{
            //    ViewBag.Message = "Comic removed from pull list!";
            //    return RedirectToAction("PullList");
            //}
            //else
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
            //}
            //}
            //else
            //{
            //    return View(Index());
            //}
        //}

        // GET: Customer/Delete/5
        //public ActionResult Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    Customer customer = db.Customers.Find(id);
        //    if (customer == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(customer);
        //}

        //// POST: Customer/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Customer customer = db.Customers.Find(id);
        //    db.Customers.Remove(customer);
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
