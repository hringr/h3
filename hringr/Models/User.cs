using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hringr.Models
{
    public class User
    {
        public int ID { get; set; }
        public string userName { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
    }
}