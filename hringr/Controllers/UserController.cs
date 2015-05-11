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
            var users = (from x in db.Users
                         orderby x.UserName ascending 
                         select x);
            return View(users);
        }
        public ActionResult Details(string u)
        {
            var user = (from x in db.Users
                        where x.UserName.Equals(u)
                        select x).SingleOrDefault();
            return View(user);
        }
	}
}