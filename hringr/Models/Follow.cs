

namespace hringr.Models
{
    public class Follow
    {
        public int ID { get; set; }
        //public string followerID { get; set; }
        public virtual ApplicationUser user { get; set; }
        public virtual ApplicationUser followee { get; set; }
        public bool deleted { get; set; }

    }
}