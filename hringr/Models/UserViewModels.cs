using System.Collections.Generic;

namespace hringr.Models
{
    public class UserViewModels
    {
        public IEnumerable<ApplicationUser> users { get; set; }
        public ApplicationUser user { get; set; }
        public IEnumerable<Post> posts { get; set; }  
    }
}