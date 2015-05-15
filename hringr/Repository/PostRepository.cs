using System.Collections.Generic;
using System.Linq;
using hringr.Models;
using System;
using System.Configuration;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using WebGrease.Css.Ast.Selectors;
using Microsoft.AspNet.Identity;

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

        public IEnumerable<Post> GetAllPosts(string uid, ApplicationDbContext m_db)
        {
            //var result = (
            //    from p in m_db.Posts
            //    join f in m_db.Follows on p.user.Id equals f.followee.Id
            //    where f.user.Id.Equals(uid) 
            //    orderby p.date descending
            //    select p
            //    ).ToList().Take(10);

            //var result = (
            //    from f in m_db.Follows
            //    join p in m_db.Posts on f.followee.Id equals p.user.Id
            //    where f.followee.Id.Equals(uid)
            //    && p.user.Id.Equals(f.user.Id)
            //    //&& f.deleted.Equals(false)
            //    orderby p.date descending 
            //    select p
            //    ).ToList().Take(10);

            var followees = (
                from f in m_db.Follows
                where f.user.Id.Equals(uid)
                && f.deleted.Equals(false)
                select f.followee).ToList();

            IList<Post> posts = new List<Post>();

            foreach (var f in followees)
            {
                var x = GetPostListByUserId(f.Id, m_db);
                x.CopyItemsTo(posts);
            }
            var results = (
                from x in posts
                orderby x.date descending
                select x
                ).ToList();

            return results;
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
            var result = (
                from x in m_db.Posts
                where x.user.Id.Equals(id)
                orderby x.date descending
                select x
                ).ToList();
            return result;
        }
        public IList<Post> GetPostListByUserId(string id, ApplicationDbContext m_db)
        {
            var result = (
                from x in m_db.Posts
                where x.user.Id.Equals(id)
                orderby x.date descending
                select x
                ).ToList();
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

        public Like GetLikeById(int id, ApplicationDbContext m_db)
        {
            var result = (
                from x in m_db.Likes
                where x.ID.Equals(id)
                select x
                ).FirstOrDefault();
            return result;
        }

        public IEnumerable<Like> GetLikes(int id, ApplicationDbContext m_db)
        {
            var result = 
                from lk in m_db.Likes
                where lk.postID == id
                select lk;
            return result;
        }

        // Oh dear god I created a monster
        // Bad Simmi! BAD!
        public IEnumerable<Post> GetPostLikes(IEnumerable<Post> posts, ApplicationDbContext m_db)
        {
            var result = posts;
            foreach (var post in result)
            {
                post.likes = GetLikes(post.ID, m_db);
                post.dislikes = GetDislikes(post.ID, m_db);
            }
            return result;
        }

        public Dislike GetDislikeById(int id, ApplicationDbContext m_db)
        {
            var result = (
                from x in m_db.Dislikes
                where x.ID.Equals(id)
                select x
                ).FirstOrDefault();
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

        public bool IsLikeValid(Like lk, ApplicationDbContext m_db)
        {
            return lk.valid;
        }

        public bool userLikedBefore(Like lk, ApplicationDbContext m_db)
        {
            foreach (var like in m_db.Likes)
            {
                if (like.postID.Equals(lk.postID) && like.userID.Equals(lk.userID))
                {
                    return true;
                }
            }
            return false;
        }

        public bool userDislikedBefore(Dislike lk, ApplicationDbContext m_db)
        {
            foreach (var dislike in m_db.Dislikes)
            {
                if (dislike.postID.Equals(lk.postID) && dislike.userID.Equals(lk.userID))
                {
                    return true;
                }
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