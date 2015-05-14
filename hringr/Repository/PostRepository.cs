using System.Collections.Generic;
using System.Linq;
using hringr.Models;
using System;
using System.Web.Mvc;

namespace hringr.Repository
{
    public class PostRepository
    {
        //private static PostRepository _instance;

        //public static PostRepository Instance
        //{
        //    get
        //    {
        //        if (_instance == null)
        //            _instance = new PostRepository();
        //        return _instance;
        //    }
        //}

        //private ApplicationDbContext m_db = new ApplicationDbContext();

        public IEnumerable<Post> GetAllPosts(ApplicationDbContext m_db)
        {
            var result = (from n in m_db.Posts
                orderby n.date descending
                select n).ToList().Take(10);
            return result;
        }

        public Post GetPostById(int? id, ApplicationDbContext m_db)
        {
            var result = (from n in m_db.Posts
                where n.ID == id
                select n).FirstOrDefault();
            return result;
        }

        public IEnumerable<Post> GetPostByUserId(string id, ApplicationDbContext m_db)
        {
            var result = (from x in m_db.Posts
                where x.user.Id.Equals(id)
                orderby x.date descending
                select x).ToList();
            return result;
        }

        public void AddPost(Post n, ApplicationDbContext m_db)
        {
            m_db.Posts.Add(n);
            m_db.SaveChanges();
        }

        public void UpdatePost(Post n, ApplicationDbContext m_db)
        {
            Post t = GetPostById(n.ID, m_db);
            if (t != null)
            {
                t.title = n.title;
                t.link = n.link;
                t.text = n.text;
                t.date = n.date;
                t.categoryID = n.categoryID;
                m_db.SaveChanges();
            }
        }

        public void DeletePost(Post n, ApplicationDbContext m_db)
        {
            m_db.Posts.Remove(n);
            m_db.SaveChanges();
        }

        public IEnumerable<Like> GetLikes(int id, ApplicationDbContext m_db)
        {
            var result = from lk in m_db.Likes
                where lk.postID == id
                select lk;
            return result;
        }

        public IEnumerable<Dislike> GetDislikes(int id, ApplicationDbContext m_db)
        {
            var result = from lk in m_db.Dislikes
                where lk.postID == id
                select lk;
            return result;
        }

        public void AddLike(Like lk, ApplicationDbContext m_db)
        {
            m_db.Likes.Add(lk);
            m_db.SaveChanges();
        }

        public void AddDislike(Dislike lk, ApplicationDbContext m_db)
        {
            m_db.Dislikes.Add(lk);
            m_db.SaveChanges();
        }

        public bool userLikedBefore(int id, ApplicationUser user, ApplicationDbContext m_db)
        {
            foreach (var like in m_db.Likes)
            {
                if (like.postID == id && like.user == user)
                    return true;
            }
            return false;
        }

        public IEnumerable<SelectListItem> GetCategories(ApplicationDbContext m_db)
        {
            return m_db.Categories.Select(c => new SelectListItem
            {
                Text = c.name,
                Value = c.ID.ToString()
            });
        }

        public ApplicationUser GetCurrentUser(string id, ApplicationDbContext m_db)
        {
            var result = (from x in m_db.Users
                where x.Id.Equals(id)
                select x).FirstOrDefault();
            return result;
        }
    }
}