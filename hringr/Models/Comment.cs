﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hringr.Models
{
    public class Comment
    {
        public int ID { get; set; }
        public int postID { get; set; }
        public int userID { get; set; }
        public string text { get; set; }
        public DateTime date { get; set; }
    }
}