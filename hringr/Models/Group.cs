using System.Collections.Generic;

namespace hringr.Models
{
    public class Group
    {
        public int ID { get; set; }
        public string name { get; set; }
        public virtual ICollection<GroupMember> member { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}