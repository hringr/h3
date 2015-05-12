using System.Collections.Generic;
using System.Linq;
using hringr.Models;

namespace hringr.Repository
{
    public class SearchRepository
    {
        private static SearchRepository _instance;
        public static SearchRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SearchRepository();
                return _instance;
            }
        }

        static readonly ApplicationDbContext m_db = new ApplicationDbContext();

        //public IEnumerable<ApplicationUser> SearchForUsers(string str)
        //{
        //    var results = from x in m_db.Users
        //                  where x.
        //} 
	}
}