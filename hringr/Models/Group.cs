using System.Collections.Generic;

namespace hringr.Models
{
    public class Group
    {
        public int ID { get; set; }
        public string name { get; set; }
        public virtual ICollection<GroupMember> member { get; set; }
        public virtual IEnumerable<ApplicationUser> users { get; set; }
        public virtual IEnumerable<Post> posts { get; set; } 
    }
}