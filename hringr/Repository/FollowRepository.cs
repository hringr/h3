using System.Dynamic;
using System.Linq;
using hringr.Models;

namespace hringr.Repository
{
    public class FollowRepository : Repo
    {
        private static PostRepository _instance;

        public static PostRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new PostRepository();
                return _instance;
            }
        }

        private ApplicationDbContext m_db = GetDbContext();

        //static ApplicationDbContext m_db = new ApplicationDbContext();

        public void AddFollow(Follow model)
        {
            m_db.Follows.Add(model);
            m_db.SaveChanges();
        }
    }
}