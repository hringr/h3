

namespace hringr.Models
{
    public class Follow
    {
        public int ID { get; set; }
        public int followerID { get; set; }
        public int followeeID { get; set; }

        public virtual ApplicationUser user { get; set; }
    }
}