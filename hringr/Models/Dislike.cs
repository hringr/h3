
using System.ComponentModel;

namespace hringr.Models
{
    public class Dislike
    {
        public Dislike()
        {
            valid = true;
        }

        public int ID { get; set; }
        
        public int postID { get; set; }

        public string userID { get; set; }
        
        public bool valid { get; set; }
    }
}