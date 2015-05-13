using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hringr.Models;
using hringr.Repository;
using Microsoft.AspNet.Identity;

namespace hringr.Controllers
{
    public class UserController : Controller
    {
        private readonly UserRepository userRepo = new UserRepository();
        private readonly PostRepository postRepo = new PostRepository();

        //
        // GET: /User/
        public ActionResult Index()
        {
            var users = userRepo.GetAllUsers();
            return View(users);
        }
        public ActionResult Details(string u)
        {
            var user = userRepo.GetUserByUserName(u);
            var posts = postRepo.GetPostByUserId(user.Id);
            UserViewModels viewModel = new UserViewModels();
            viewModel.user = user;
            viewModel.posts = posts;
            return View(viewModel);
        }
        //public ActionResult Follow(string u)
        //{
        //    Follow follow = new Follow();
        //    string currentUserId = User.Identity.GetUserId();
        //    var user = postRepo.GetCurrentUser(currentUserId);
        //    var followee = userRepo.GetUserByUserName(u);
        //    follow.user = user;
        //    follow.followee = followee;
        //    follow.deleted = false;
        //    userRepo.AddFollow(follow);
        //    return RedirectToAction("Details", new { u = followee.UserName });
        //}


	}
}