using System.Collections.Generic;
using System.Linq;
using hringr.Models;

namespace hringr.Repository
{
    public class UserRepository
    {
        private static UserRepository _instance;
        public static UserRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserRepository();
                return _instance;
            }
        }

        static ApplicationDbContext m_db = new ApplicationDbContext();

    //    public static IEnumerable<ApplicationUser> GetAllUsers()
    //    {
            
    //    }
    }
}