using System;
using System.ComponentModel.DataAnnotations;

namespace hringr.Models
{
    public class Post
    {
        public Post()
        {
            date = DateTime.Now;
        }

        public int ID { get; set; }
       
        public int categoryID { get; set; }
        public DateTime date { get; set; }

        [Required(ErrorMessage = "Title required")]
        public string title { get; set; }

        [Required(ErrorMessage = "Text required")]
        public string text { get; set; }

        [Required(ErrorMessage = "Link required")]
        public string link { get; set; }

        public int groupID { get; set; }
        
        public virtual ApplicationUser user { get; set; }
    }
}