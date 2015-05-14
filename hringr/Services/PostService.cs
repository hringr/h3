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

        public IEnumerable<Post> GetMostRecentPosts(int postsPerPage, int pageNumber)
        {
            var skip = postsPerPage * pageNumber;
            var posts = (from up in _db.Posts
                         orderby up.date descending
                         select up).Skip(skip).Take(postsPerPage).ToList();

            return posts;
        }
        public IEnumerable<Post> GetMostRecentPostsForUser(ApplicationUser author, int postsPerPage, int pageNumber)
        {
            var skip = postsPerPage * pageNumber;
            var posts = (from up in _db.Posts
                         where up.user == author
                         orderby up.date descending
                         select up).Skip(skip).Take(postsPerPage).ToList();
            return posts;
        }
    }
}