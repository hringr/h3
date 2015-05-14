using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hringr.Models;
using hringr.Repository;
using hringr.Services;

namespace hringr.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return RedirectToAction("Recent");
        }

        public ActionResult Recent(int? pageNumber)
        {
            pageNumber = pageNumber ?? 0;
            var service = new PostService(null);
            var postsPerPage = 8;
            var posts = service.GetMostRecentPosts(postsPerPage, pageNumber.Value);
            // 
            if (Request.IsAjaxRequest())
            {
                return PartialView("_Posts", posts);
            }
            else
            {
                return View("Index", posts);
            }
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

    }
}