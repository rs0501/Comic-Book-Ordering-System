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
    public class ComicsController : Controller
    {
        //private ApplicationDbContext db = new ApplicationDbContext();

        private ComicManager _comicManager = new ComicManager();
        private CustomerManager _customerManager = new CustomerManager();

        /// <summary>
        /// Application DB context
        /// </summary>
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<ApplicationUser> UserManager { get; set; }

        // GET: Comics
        public ActionResult Index()
        {
            return View(_comicManager.RetrieveAvailableComics(true));
        }

        // GET: Comics/Details/5
        public ActionResult Details(int? id)
        {
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

        // GET: Comics/PullList
        public ActionResult PullList()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
            var user = UserManager.FindById(User.Identity.GetUserId());
            string email = user.Email;

            int? id = _customerManager.RetrieveCustomerIdByEmail(email);

            //var customerId = "";
            //customerId = _customerManager.CurrentCustomersList().Find(c => c.CustomerID == id);

            var pullList = _comicManager.PullListForCustomer(id);

            if (null == pullList)
            {
                ViewBag.Message = "Add comics to your pull list!";
                //return View(Index());
            }
            
            return View(pullList);
        }


        // GET: Comics/AddToPull/5
        public ActionResult AddToPull(int? id)
        {
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

        // POST: Comics/AddToPull/5
        [HttpPost]
        [ActionName("AddToPull")]
        public ActionResult AddToPullConfirmed(int? id)
        {
            if (ModelState.IsValid)
            {
                this.ApplicationDbContext = new ApplicationDbContext();
                this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
                var user = UserManager.FindById(User.Identity.GetUserId());
                string email = user.Email;

                var customerId = _customerManager.RetrieveCustomerIdByEmail(email);
                var comicId = id;
                //var comic = _comicManager.RetrieveAvailableComics(true).Find(c => c.ComicID == comicId);
                //comicID = comic.ComicID;

                //try
                //{
                    //_comicManager.AddComicToPullList(customerId, comicId);
                    //return View(PullList());
                if (_comicManager.AddComicToPullList(customerId, comicId) == true)
                {
                    ViewBag.Message = "Comic added to pull list!";
                    return RedirectToAction("Index");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                }
                //}
                //catch (Exception ex)
                //{

                //    ViewBag.Message = "An error occured adding comic to pull list. " + ex.Message + ex.StackTrace;
                //} 
            }
            else
            {
                return View(Index());
            }

            //return View(PullList());
        }

        // GET: Comics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comics/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ComicID,ComicType,DistributorID,Title,Authors,Issue,Publisher,Rating,Description,Quantity,Price,Status")] Comic comic)
        {
            if (ModelState.IsValid)
            {
                if (_comicManager.AddComicToInventory(comic.Title, comic.Issue, comic.Authors, comic.Publisher, comic.ComicType, comic.DistributorID,
                                                        comic.Rating, comic.Description, comic.Quantity, comic.Price, comic.Status) == true)
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
                return View(comic);
            }

        }

        // GET: Comics/Edit/5
        public ActionResult Edit(int? id)
        {
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

        // POST: Comics/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ComicID,ComicType,DistributorID,Title,Authors,Issue,Publisher,Rating,Description,Quantity,Price,Status")] Comic newComic)
        {
            if (ModelState.IsValid)
            {
                var oldComic = _comicManager.RetrieveAvailableComics(true).Find(c => c.ComicID == newComic.ComicID);
                if(_comicManager.UpdateComicInfo(oldComic, newComic.Title, newComic.Issue, newComic.Authors, newComic.Publisher, newComic.ComicType, newComic.DistributorID, 
                                                    newComic.Rating, newComic.Description, newComic.Quantity, newComic.Price, newComic.Status) == true)
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
                return View(newComic);
            }
        }

        // GET: Comics/Remove/5
        public ActionResult RemoveFromPull(int? id)
        {
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
        [HttpPost]
        [ActionName("RemoveFromPull")]
        //[ValidateAntiForgeryToken]
        public ActionResult RemoveFromPullConfirmed(int? id)
        {
            //if (ModelState.IsValid)
            //{
                this.ApplicationDbContext = new ApplicationDbContext();
                this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
                var user = UserManager.FindById(User.Identity.GetUserId());
                string email = user.Email;

                var customerId = _customerManager.RetrieveCustomerIdByEmail(email);
                var comicId = id;

                if (_comicManager.RemoveComicFromPullList(comicId, customerId) == true)
                {
                    ViewBag.Message = "Comic removed from pull list!";
                    return RedirectToAction("PullList");
                }
                else
                {
                    return new HttpStatusCodeResult(HttpStatusCode.ServiceUnavailable);
                }
            //}
            //else
            //{
            //    return View(Index());
            //}
        }

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
