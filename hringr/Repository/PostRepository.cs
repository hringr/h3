using System.Collections.Generic;
using System.Linq;
using hringr.Models;

namespace hringr.Repository
{
    public class PostRepository
    {
        static ApplicationDbContext m_db = new ApplicationDbContext();

        public static IEnumerable<Post> GetAllPosts()
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
                t.category = n.category;
                m_db.SaveChanges();
            }
        }

        public void DeletePost(Post n)
        {
            m_db.Posts.Remove(n);
            m_db.SaveChanges();
        }

        public IEnumerable<Like> GetLikes(int id)
        {
            var result = from lk in m_db.Likes
                         where lk.postID == id
                         select lk;
            return result;
        }

        public IEnumerable<Dislike> GetDislikes(int id)
        {
            var result = from lk in m_db.Dislikes
                         where lk.postID == id
                         select lk;
            return result;
        }

        public void AddLike(Like lk)
        {
            int likeID = 1;

            /* if (m_db.Count() > 0)
             {
                 likeID = m_db.Max(x => x.ID) + 1;
             }*/

            lk.ID = likeID;
            m_db.Likes.Add(lk);
            m_db.SaveChanges();

        }

        public void AddDislike(Dislike lk)
        {
            int dislikeID = 1;

            /* if (m_db.Count() > 0)
             {
                 likeID = m_db.Max(x => x.ID) + 1;
             }*/

            lk.ID = dislikeID;
            m_db.Dislikes.Add(lk);
            m_db.SaveChanges();

        }

         /*public bool userLikedBefore(int id, string username)
         {
             foreach (var like in m_db.Likes)
             {
                 if (like.postID == id && like.user == username)
                     return true;
             }
             return false;
         }*/
    }
}