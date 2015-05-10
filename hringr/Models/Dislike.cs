
namespace hringr.Models
{
    public class Dislike
    {
        public int ID { get; set; }
        
        public int postID { get; set; }

        public virtual ApplicationUser user { get; set; }
    }
}