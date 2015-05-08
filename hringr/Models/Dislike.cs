using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hringr.Models
{
    public class Dislike
    {
        public int ID { get; set; }
        
        public int postID { get; set; }

        public virtual ApplicationUser user { get; set; }
    }
}