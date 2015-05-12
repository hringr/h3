using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace hringr.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FullName { get; set; }

        public virtual ICollection<Post> Posts { get; set; } 
    }

    public interface IAppDataContext
    {
        IDbSet<Category> Categories { get; set; }

        IDbSet<Post> Posts { get; set; }

        IDbSet<Follow> Follows { get; set; }

        IDbSet<Group> Groups { get; set; }

        IDbSet<Like> Likes { get; set; }

        IDbSet<Dislike> Dislikes { get; set; }

        IDbSet<Comment> Comments { get; set; }

        IDbSet<GroupMember> GroupMembers { get; set; }

        int SaveChanges();
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IAppDataContext
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public IDbSet<Category> Categories { get; set; }

        public IDbSet<Post> Posts { get; set; }

        public IDbSet<Follow> Follows { get; set; }

        public IDbSet<Group> Groups { get; set; }

        public IDbSet<Like> Likes { get; set; }

        public IDbSet<Dislike> Dislikes { get; set; }

        public IDbSet<Comment> Comments { get; set; }

        public IDbSet<GroupMember> GroupMembers { get; set; }
    }
}