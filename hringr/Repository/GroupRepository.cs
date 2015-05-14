using System.Linq;
using hringr.Models;
using System.Collections.Generic;

namespace hringr.Repository
{
    public class GroupRepository
    {
        static ApplicationDbContext m_db = new ApplicationDbContext();
        private static GroupRepository _instance;

        public IEnumerable<Group> GetAllGroups()
        {
            var result = (from n in m_db.Groups
                          orderby n.name ascending
                          select n).ToList().Take(10); ;
            return result;
        }

        public Group GetGroupById(int? id)
        {
            var result = (from n in m_db.Groups
                          where n.ID == id
                          select n).FirstOrDefault();
            return result;
        }

        public IEnumerable<Group> GetGroupByUserId(string id)
        {
            var result = (from x in m_db.Groups
                          where x.user.Id == id
                          orderby x.name ascending
                          select x).ToList();
            return result;
        }

        public void CreateGroup(Group n)
        {
            m_db.Groups.Add(n);
            m_db.SaveChanges();
        }

        public void UpdateGroup(Group n)
        {
            Group t = GetGroupById(n.ID);
            if (t != null)
            {
                t.name = n.name;
                t.user = n.user;
                m_db.SaveChanges();
            }
        }

        public void DeleteGroup(Group n)
        {
            m_db.Groups.Remove(n);
            m_db.SaveChanges();
        }
    }
}