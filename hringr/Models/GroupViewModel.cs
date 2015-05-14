using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hringr.Models
{
    public class GroupViewModel
    {
        public IEnumerable<Group> groups { get; set; }
        public IEnumerable<ApplicationUser> users { get; set; }
        public ApplicationUser user { get; set; }
        public virtual GroupMember member { get; set; }
    }
}