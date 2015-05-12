using System.Collections.Generic;
using System.Linq;
using hringr.Models;

namespace hringr.Services
{
    public class PostService
    {
        private readonly IAppDataContext _db;
        public PostService(IAppDataContext context)
        {
            _db = context ?? new ApplicationDbContext();
        }

        public List<Post> GetMostRecentPosts(int postsPerPage, int pageNumber)
        {
            var skip = postsPerPage * (pageNumber - 1);
            var posts = (from up in _db.Posts
                         orderby up.date descending
                         select up).Skip(skip).Take(postsPerPage).ToList();

            return posts;
        }
        public List<Post> GetMostRecentPostsForUser(ApplicationUser author)
        {
            return new List<Post>();
        }
    }
}