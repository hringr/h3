using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using hringr.Models;

namespace hringr.Repository
{
    public class UserRepository
    {
        //private static UserRepository _instance;

        //public static UserRepository Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //            _instance = new UserRepository();
        //        return _instance;
        //    }
        //}

        //private ApplicationDbContext m_db = new ApplicationDbContext();

        public IEnumerable<ApplicationUser> GetAllUsers(ApplicationDbContext m_db)
        {
            var result = 
                (from x in m_db.Users
                orderby x.UserName ascending
                select x);

            return result;
        }

        public ApplicationUser GetUserByUserName(string usr, ApplicationDbContext m_db)
        {
            var result = 
                (from x in m_db.Users
                where x.UserName.Equals(usr)
                select x).SingleOrDefault();

            return result;
        }

        public void AddFollow(Follow model, ApplicationDbContext m_db)
        {
            if (model.deleted.Equals(false))
            {
                m_db.Follows.Add(model);
            }
            else
            {
                model.deleted = false;
            }
            m_db.SaveChanges();
        }

        public void RemoveFollow(string uID, string fID, ApplicationDbContext m_db)
        {
            var result = 
                (from x in m_db.Follows
                where x.user.Id.Equals(uID)
                && x.followee.Id.Equals(fID)
                select x).First();

            result.deleted = true;
            m_db.SaveChanges();
        }

        public Follow FindFollow(string uID, string fID, ApplicationDbContext m_db)
        {
            var result =
                (from x in m_db.Follows
                 where x.user.Id.Equals(uID)
                 && x.followee.Id.Equals(fID)
                 select x).SingleOrDefault();

            return result;
        }
    }
}