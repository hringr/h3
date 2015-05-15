using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using hringr.Models;
using hringr.Repository;
using Microsoft.AspNet.Identity;

namespace hringr.Controllers
{
    public class GroupController : Controller
    {
        private ApplicationDbContext m_db = new ApplicationDbContext();
        private readonly GroupRepository groupRepo = new GroupRepository();
        private readonly UserRepository userRepo = new UserRepository();

        // GET: /Group/
        public ActionResult Index()
        {
            var groups = groupRepo.GetAllGroups();
            return View(groups);
        }

        // GET: /Group/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var group = groupRepo.GetGroupById(id);
            if (group == null)
            {
                return HttpNotFound();
            }

            group.posts = groupRepo.GetGroupPosts(id);

            return View(group);
        }

        // GET: /Group/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Group/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,name")] Group group)
        {
            if (ModelState.IsValid)
            {
                groupRepo.CreateGroup(group);
                return RedirectToAction("Index");
            }

            return View(group);
        }

        // GET: /Group/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = groupRepo.GetGroupById(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: /Group/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,name")] Group group)
        {
            if (ModelState.IsValid)
            {
                groupRepo.UpdateGroup(group);
                return RedirectToAction("Index");
            }
            return View(group);
        }

        // GET: /Group/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Group group = groupRepo.GetGroupById(id);
            if (group == null)
            {
                return HttpNotFound();
            }
            return View(group);
        }

        // POST: /Group/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Group group = groupRepo.GetGroupById(id);
            groupRepo.DeleteGroup(group);
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

        public ActionResult AddMemberToGroup(int id)
        {
            var uID = User.Identity.GetUserId();
            GroupMember gm = new GroupMember
            {
                groupID = id,
                userID = uID
            };


            groupRepo.AddToGroup(gm);

            return RedirectToAction("Details", "Group", new {id = gm.groupID});
        }

        public ActionResult RemoveMemberFromGroup(int id)
        {
            var user = userRepo.GetUserByUserName(User.Identity.GetUserName(), m_db);
            var group = groupRepo.GetGroupById(id);
            string userID = user.Id;
            int groupID = group.ID;
            groupRepo.RemoveFromGroup(userID, groupID);
            return RedirectToAction("Details", "Group", new {id = groupID});
        }

        public ActionResult Members(int id)
        {
            var model = groupRepo.GetMembers(id);
            return View(model);
        }
    }
}
