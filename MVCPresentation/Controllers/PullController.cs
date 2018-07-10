using ComicBookLogic;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MVCPresentation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCPresentation.Controllers
{
    public class PullController : Controller
    {
        /// <summary>
        /// Application DB context
        /// </summary>
        protected ApplicationDbContext ApplicationDbContext { get; set; }

        /// <summary>
        /// User manager - attached to application DB context
        /// </summary>
        protected UserManager<ApplicationUser> UserManager { get; set; }
        // GET: Pull
        //public ActionResult Index()
        //{
        //    return View();
        //}

        private ComicManager _comicManager;
        private CustomerManager _customerManager;

        public PullController(ComicManager comicMgr)
        {
            _comicManager = comicMgr;
        }

        public RedirectToRouteResult AddToPull(int? id, string returnURL)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            //}
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
            var user = UserManager.FindById(User.Identity.GetUserId());
            string email = user.Email;
            //int? comicID = 0;

            var customerId = _customerManager.RetrieveCustomerIdByEmail(email);
            var comicId = id;
            //var comic = _comicManager.RetrieveAvailableComics(true).Find(c => c.ComicID == comicId);
            //comicID = comic.ComicID;

            try
            {
                _comicManager.AddComicToPullList(customerId, comicId);
                //if ()
                //{
                //    ViewBag.Message = "Comic added to pull list!";
                //}
            }
            catch (Exception ex)
            {

                ViewBag.Message = "An error occured adding comic to pull list. " + ex.Message + ex.StackTrace;
            }

            return RedirectToAction("Index", new { returnURL });
        }
    }
}