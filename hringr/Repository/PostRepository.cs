using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using hringr.DAL;
using hringr.Models;

namespace hringr.Repository
{
    public class PostRepository
    {
        HringrContext m_db = new HringrContext();

        public IEnumerable<Post> GetAllPosts()
        {
            var result = (from n in m_db.Posts
                orderby n.date descending 
                select n).Take(10);
            return result;
        }

        public Post GetPostById(int id)
        {
            var result = (from n in m_db.Posts
                where n.ID == id
                select n).SingleOrDefault();
            return result;
        }

        public void AddPost(Post n)
        {
            m_db.Posts.Add(n);
            m_db.SaveChanges();
        }

        public void UpdatePost(Post n)
        {
            Post t = GetPostById(n.ID);
            if (t != null)
            {
                t.title = n.title;
                t.link = n.link;
                t.text = n.text;
                m_db.SaveChanges();
            }
        }

        public void DeletePost(Post n)
        {
            m_db.Posts.Remove(n);
            m_db.SaveChanges();
        }
    }
}