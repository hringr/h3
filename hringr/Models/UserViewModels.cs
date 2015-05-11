using System.Collections.Generic;

namespace hringr.Models
{
    public class UserViewModels
    {
        public IEnumerable<ApplicationUser> user { get; set; }
        public IEnumerable<Post> posts { get; set; }  
    }
}