using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hringr.Models;

namespace hringr.Controllers
{
    public class UserController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /User/
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Details(string user)
        {
            var u = (from x in db.Users
                     where x.UserName.Equals(user)
                     select x).SingleOrDefault();
            return View(u);
        }
	}
}