using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hringr.Models
{
    public class Follow
    {
        public int ID { get; set; }
        public int followerID { get; set; }
        public int followeeID { get; set; }
    }
}