using System.Data.Entity;
using hringr.Models;

namespace hringr.Tests
{
    /// <summary>
    /// This is an example of how we'd create a fake database by implementing the 
    /// same interface that the BookeStoreEntities class implements.
    /// </summary>
    public class MockDatabase : IAppDataContext
    {
        /// <summary>
        /// Sets up the fake database.
        /// </summary>
        public MockDatabase()
        {
            // We're setting our DbSets to be InMemoryDbSets rather than using SQL Server.
            this.Posts = new InMemoryDbSet<Post>();
        }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<Follow> Follows { get; set; }

        public IDbSet<Group> Groups { get; set; }

        public IDbSet<Like> Likes { get; set; }

        public IDbSet<Dislike> Dislikes { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<GroupMember> GroupMembers { get; set; }

        public int SaveChanges()
        {
            // Pretend that each entity gets a database id when we hit save.
            int changes = 0;
            // changes += DbSetHelper.IncrementPrimaryKey<Author>(x => x.AuthorId, this.Authors);
            // changes += DbSetHelper.IncrementPrimaryKey<Book>(x => x.BookId, this.Books);

            return changes;
        }

        public void Dispose()
        {
            // Do nothing!
        }
    }
}