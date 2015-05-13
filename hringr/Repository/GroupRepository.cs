using System.Linq;
using hringr.Models;

namespace hringr.Repository
{
    public class GroupRepository
    {
        ApplicationDbContext m_db = new ApplicationDbContext();

        public Group GetGroupById(int id)
        {
            var result = (from n in m_db.Groups
                          where n.ID == id
                          select n).SingleOrDefault();
            return result;
        }

        public void CreateGroup(Group n)
        {
            m_db.Groups.Add(n);
            m_db.SaveChanges();
        }

        public void DeleteGroup(Group n)
        {
            m_db.Groups.Remove(n);
            m_db.SaveChanges();
        }
    }
}