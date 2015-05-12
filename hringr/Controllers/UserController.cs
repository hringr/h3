using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hringr.Models;
using hringr.Repository;

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
	}
}