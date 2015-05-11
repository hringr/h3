using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace hringr.Models
{
    public class CreatePostViewModel
    {
        public Post post { get; set; }
        public IEnumerable<SelectListItem> categories { get; set; }
    }
}