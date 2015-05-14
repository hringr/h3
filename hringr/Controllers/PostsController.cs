using System;
using System.Data.Entity;
using System.EnterpriseServices;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Web.Security;
using hringr.Models;
using hringr.Repository;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;

namespace hringr.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext m_db = new ApplicationDbContext();
        private PostRepository postRepo = new PostRepository();
        private UserRepository userRepo = new UserRepository();

        // GET: Posts
        [Authorize]
        public ActionResult Index()
        {
            var posts = postRepo.GetAllPosts(m_db);
            return View(posts);
        }

        // GET: Posts/Details/5
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = postRepo.GetPostById(id, m_db);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public ActionResult Create()
        {
            // Controller myndi taka við postRepo.GetCategories() sem væru af taginu Category
            // Og fara svo í gegnum hvern og einn og búa til lista af SelectListItem

            //IEnumerable<Category> categories = postRepo.GetCategories();


            CreatePostViewModel viewModel = new CreatePostViewModel();
            viewModel.categories = postRepo.GetCategories(m_db);
            viewModel.post = new Post();

            return View(viewModel);
        
        }

        // POST: Posts/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,categoryID,userID,date,title,text,link,groupID")] Post post)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                ApplicationUser currentUser = postRepo.GetCurrentUser(currentUserId, m_db);
                post.user = currentUser;
                postRepo.AddPost(post, m_db);
                return RedirectToAction("Index");
            }
            
            return View();
        }

        // GET: Posts/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreatePostViewModel viewModel = new CreatePostViewModel();
            viewModel.categories = postRepo.GetCategories(m_db);
            Post post = postRepo.GetPostById(id, m_db);
            post.date = DateTime.Now;
            viewModel.post = post;
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(viewModel);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,categoryID,userID,date,title,text,link,groupID")] Post post)
        {
            if (ModelState.IsValid)
            {
                postRepo.UpdatePost(post, m_db);
                return RedirectToAction("Index");
            }
            return View(post);
        }

        // GET: Posts/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var post = postRepo.GetPostById(id, m_db);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            /*Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
            db.SaveChanges();*/
            var post = postRepo.GetPostById(id, m_db);
            postRepo.DeletePost(post, m_db);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                m_db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult AddLike(int postingID)
        {
            if (postingID != 0)
            {
                Like lk = new Like
                {
                    postID = postingID,
                    userID = User.Identity.GetUserId()
                };

                if (!postRepo.userLikedBefore(lk, m_db))
                    postRepo.AddLike(lk, m_db);

                return Json(lk, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Index();
            }
        }

        public ActionResult AddDislike(int postingID)
        {
            if (postingID != 0)
            {
                Dislike lk = new Dislike
                {
                    postID = postingID,
                    user = userRepo.GetUserByUserName(User.Identity.GetUserName(), m_db)
                };



                //if (!postRepo.userLikedBefore(lk, m_db))
                //    postRepo.AddDislike(lk, m_db);

                return Json(lk, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Index();
            }
        }

        public ActionResult GetLikes(int postId)
        {
            var like = postRepo.GetLikes(postId, m_db);
            return Json(like, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDislikes(int postId)
        {
            var dislike = postRepo.GetLikes(postId, m_db);
            return Json(dislike, JsonRequestBehavior.AllowGet);
        }
    }
}
