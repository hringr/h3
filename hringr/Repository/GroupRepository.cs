using System.Linq;
using hringr.Models;
using System.Collections.Generic;
using System.Web.Mvc;

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

        public GroupMember FindGroup(string uID, int gID)
        {
            var result =(from x in m_db.GroupMembers
                             where x.user.Id == uID
                             && x.groups.ID == gID
                             select x).SingleOrDefault();
            return result;
        }

        public void AddToGroup(GroupMember model)
        {
            if(model.deleted.Equals(null))
            {
                m_db.GroupMembers.Add(model);
            }
            else
            {
                model.deleted = false;
            }
            m_db.SaveChanges();
        }

        public void RemoveFromGroup(string uID, int gID)
        {
            var result = (from x in m_db.GroupMembers
                          where x.user.Id == uID
                          && x.groups.ID == gID
                          select x).First();
            result.deleted = true;
            m_db.SaveChanges();
        }

        public IEnumerable<SelectListItem> GetMembers()
        {
            return m_db.GroupMembers.Select(x => new SelectListItem
                {
                    Text = x.user.FullName,
                    Value = x.user.Id
                });
        }
    }
}