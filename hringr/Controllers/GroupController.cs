using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using hringr.Models;
using hringr.Repository;

namespace hringr.Controllers
{
    public class GroupController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private GroupRepository groupRepo = new GroupRepository();

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
            Group group = groupRepo.GetGroupById(id);
            if (group == null)
            {
                return HttpNotFound();
            }
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
                /*db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();*/
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
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
