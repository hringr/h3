using System;


namespace hringr.Models
{
    public class Comment
    {
        public Comment()
        {
            date = DateTime.Now;
        }

        public int ID { get; set; }
        public int postID { get; set; }
        
        public string text { get; set; }
        public DateTime date { get; set; }

        public virtual ApplicationUser user { get; set; }
    }
}