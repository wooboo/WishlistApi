using System.Collections.Generic;

namespace WishlistApi.Model
{
    public class ListInvitations
    {
        public List<string> OwnerInvitations { get; set; }
        public List<string> ModeratorInvitations { get; set; }
        public List<string> HelperInvitations { get; set; }
    }
}