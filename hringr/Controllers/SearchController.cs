using System.Linq;
using System.Web.Mvc;
using hringr.Models;

namespace hringr.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();
        //
        // GET: /Search/
        public ActionResult Index(string q)
        {
            var users = (from u in db.Users
                         where u.UserName.Contains(q)
                         select u);

            return View(users);
        }
	}
}