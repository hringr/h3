using System.Web.Mvc;
using hringr.Models;
using hringr.Repository;
using Microsoft.AspNet.Identity;

namespace hringr.Controllers
{
    public class FollowController : Controller
    {
        private PostRepository postRepo = new PostRepository();
        private UserRepository userRepo = new UserRepository();
        private FollowRepository followRepo = new FollowRepository();

        //
        // GET: /Follow/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add(string u)
        {
            Follow follow = new Follow();
            string currentUserId = User.Identity.GetUserId();
            var user = postRepo.GetCurrentUser(currentUserId);
            var followee = userRepo.GetUserByUserName(u);
            follow.user = user;
            follow.followee = followee;
            follow.deleted = false;
            followRepo.AddFollow(follow);
            return RedirectToAction("Details", "User", new { u = followee.UserName });
        }
	}
}