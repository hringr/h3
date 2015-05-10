

namespace hringr.Models
{
    public class GroupMember
    {
        public int ID { get; set; }

        public virtual ApplicationUser user { get; set; }
    }
}