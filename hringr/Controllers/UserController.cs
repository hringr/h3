using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hringr.Models;
using hringr.Repository;
using Microsoft.Ajax.Utilities;
using Microsoft.AspNet.Identity;

namespace hringr.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository userRepo = new UserRepository();
        private readonly PostRepository postRepo = new PostRepository();
        private ApplicationDbContext m_db = new ApplicationDbContext();

        //
        // GET: /User/
        public ActionResult Index()
        {
            var users = userRepo.GetAllUsers(m_db);
            return View(users);
        }
        public ActionResult Details(string u)
        {
            var user = userRepo.GetUserByUserName(u, m_db);
            var currentUser = userRepo.GetUserByUserName(User.Identity.GetUserName(), m_db);
            var posts = postRepo.GetPostByUserId(user.Id, m_db);
            var follow = userRepo.FindFollow(currentUser.Id, user.Id, m_db);
            UserViewModels viewModel = new UserViewModels
            {
                user = user,
                posts = posts,
                follows = follow
            };
            return View(viewModel);
        }
        public ActionResult Follow(string u)
        {
            var currentUser = userRepo.GetUserByUserName(User.Identity.GetUserName(), m_db);
            var followee = userRepo.GetUserByUserName(u, m_db);
            var follow = userRepo.FindFollow(currentUser.Id, followee.Id, m_db);

            if (follow == null)
            {
                Follow followModel = new Follow
                {
                    user = currentUser,
                    followee = followee,
                    deleted = false
                };
                userRepo.AddFollow(followModel, m_db);
            }
            else
            {
                follow.deleted = false;
                m_db.SaveChanges();
            }
            return RedirectToAction("Details", "User", new { u = followee.UserName });
        }

        public ActionResult Unfollow(string u)
        {
            var user = userRepo.GetUserByUserName(User.Identity.GetUserName(), m_db);
            var followee = userRepo.GetUserByUserName(u, m_db);

            string userID = user.Id;
            string followeeID = followee.Id;

            userRepo.RemoveFollow(userID, followeeID, m_db);
            return RedirectToAction("Details", "User", new {u = followee.UserName});
        }
        
	}
}