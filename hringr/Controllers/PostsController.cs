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

namespace hringr.Controllers
{
    public class PostsController : Controller
    {
        private ApplicationDbContext m_db = new ApplicationDbContext();
        private PostRepository postRepo = new PostRepository();

        // GET: Posts
        [Authorize]
        public ActionResult Index()
        {
            var posts = postRepo.GetAllPosts();
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
            var post = postRepo.GetPostById(id);
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
            viewModel.categories = postRepo.GetCategories();
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
                ApplicationUser currentUser = postRepo.GetCurrentUser(currentUserId);
                post.user = currentUser;
                postRepo.AddPost(post);
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
            var post = postRepo.GetPostById(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,userID,category,date,title,text,link,groupID,category")] Post post)
        {
            if (ModelState.IsValid)
            {
                /*db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();*/
                postRepo.UpdatePost(post);
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
            var post = postRepo.GetPostById(id);
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
            var post = postRepo.GetPostById(id);
            postRepo.DeletePost(post);
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
                Like lk = new Like();

                lk.postID = postingID;


                if (!PostRepository.Instance.userLikedBefore(postingID, lk.user.UserName))
                    PostRepository.Instance.AddLike(lk);

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
                Dislike lk = new Dislike();

                lk.postID = postingID;


                if (!PostRepository.Instance.userLikedBefore(postingID, lk.user.UserName))
                    PostRepository.Instance.AddDislike(lk);

                return Json(lk, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Index();
            }
        }

        public ActionResult GetLikes(int postId)
        {
            var like = PostRepository.Instance.GetLikes(postId);
            return Json(like, JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetDislikes(int postId)
        {
            var dislike = PostRepository.Instance.GetLikes(postId);
            return Json(dislike, JsonRequestBehavior.AllowGet);
        }
    }
}
