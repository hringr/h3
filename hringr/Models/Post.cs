using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
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
        public int userID { get; set; }
        public int categoryID { get; set; }
        public DateTime date { get; set; }

        [Required(ErrorMessage = "Title required")]
        public string title { get; set; }

        public string text { get; set; }
        public string link { get; set; }
        public int groupID { get; set; }

        
    }
}