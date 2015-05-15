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

        //public IEnumerable<Group> GetGroupByUserId(string id)
        //{
        //    var result = (from x in m_db.Groups
        //                  where x.users..Id == id
        //                  orderby x.name ascending
        //                  select x).ToList();
        //    return result;
        //}

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
                //t.user = n.user;
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
                             where x.userID == uID
                             && x.groupID == gID
                             select x).SingleOrDefault();
            return result;
        }

        public void AddToGroup(GroupMember model)
        {
            m_db.GroupMembers.Add(model);
            m_db.SaveChanges();
        }

        public void RemoveFromGroup(string uID, int gID)
        {
            var result = (from x in m_db.GroupMembers
                          where x.userID == uID
                          && x.groupID == gID
                          select x).First();
            result.deleted = true;
            m_db.SaveChanges();
        }

        public IEnumerable<ApplicationUser> GetMembers(int id)
        {
            var result = (
                from x in m_db.GroupMembers
                join u in m_db.Users on x.userID equals u.Id
                where x.groupID == id
                select u
                ).ToList();
            return result;
        }

        public IEnumerable<Post> GetGroupPosts(int? id)
        {
            var results = (
                from x in m_db.Posts
                where x.groupID == id
                orderby x.date descending
                select x
                ).ToList();

            return results;
        } 
    }
}