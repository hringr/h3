using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using hringr.Models;

namespace hringr.Repository
{
    public class UserRepository
    {
        private static UserRepository _instance;

        public new static UserRepository Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new UserRepository();
                return _instance;
            }
        }

        private static readonly ApplicationDbContext m_db = new ApplicationDbContext();

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            var result = (from x in m_db.Users
                orderby x.UserName ascending
                select x);
            return result;
        }

        public ApplicationUser GetUserByUserName(string usr)
        {
            var result = (from x in m_db.Users
                where x.UserName.Equals(usr)
                select x).SingleOrDefault();
            return result;
        }
    }
}