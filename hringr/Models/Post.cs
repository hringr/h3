using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace hringr.Models
{
    public class Post
    {
        public Post()
        {
            date = DateTime.Now;
            categories = new List<SelectListItem>();
            categories.Add(new SelectListItem { Text = "-Select-", Value = "", Selected = true });
            categories.Add(new SelectListItem { Text = "News", Value = "News" });
            categories.Add(new SelectListItem { Text = "Politics", Value = "Politics" });
            categories.Add(new SelectListItem { Text = "Education", Value = "Education" });
            categories.Add(new SelectListItem { Text = "Sports", Value = "Sports" });
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

        public string category { get; set; }
        public List<SelectListItem> categories { get; set; }
        public virtual ApplicationUser user { get; set; }
    }
}