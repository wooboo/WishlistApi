using System.Collections.Generic;

namespace WishlistApi.Model.Domain
{
    /// <summary>
    /// List
    /// </summary>
    public class WishList:EntityBase
    {
        
        /// <summary>
        /// Name of a list
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Wishes in a list
        /// </summary>
        public List<Wish> Wishes { get; set; } = new List<Wish>();
        /// <summary>
        /// Owners of a list
        /// </summary>
        public List<string> Owners { get; set; } = new List<string>();
        /// <summary>
        /// Moderators
        /// </summary>
        public List<string> Moderators { get; set; } = new List<string>();
        /// <summary>
        /// Invitations
        /// </summary>
        public ListInvitations Invitations { get; set; } = new ListInvitations();
    }
}
