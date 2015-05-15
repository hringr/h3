

namespace hringr.Models
{
    public class GroupMember
    {
        public GroupMember()
        {
            deleted = false;
        }

        public int ID { get; set; }

        public string userID { get; set; }

        public int groupID { get; set; }

        public bool deleted { get; set; }
    }
}