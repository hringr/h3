using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hringr.Models;
using hringr.Repository;

namespace hringr.Controllers
{
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            return View();
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

       public ActionResult AddLike(int commentid)
       {
           if (commentid != 0)
           {
               Like lk = new Like();

               lk.postID = commentid;

               string strUser = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

               if (!String.IsNullOrEmpty(strUser))
               {
                   int slashPos = strUser.IndexOf("\\");

                   if (slashPos != -1)
                   {
                       strUser = strUser.Substring(slashPos + 1);
                   }

                   lk.user.UserName = strUser;

               }
               else
               {
                   lk.user.UserName = "Unknown user";
               }

            if (!PostRepository.Instance.userLikedBefore(commentid, lk.user.UserName))
            PostRepository.Instance.AddLike(lk);

            return Json(lk, JsonRequestBehavior.AllowGet);
            }
        else
        {
        return Index();
        }
    }

        public ActionResult GetLikes(int commentId)
        {
            var like = PostRepository.Instance.GetLikes(commentId);
            return Json(like, JsonRequestBehavior.AllowGet);
        }
    }
}