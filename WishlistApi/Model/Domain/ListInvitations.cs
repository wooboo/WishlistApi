using System.Collections.Generic;

namespace WishlistApi.Model.Domain
{
    /// <summary>
    /// Represents a structure of list invitation
    /// </summary>
    public class ListInvitations: ComplexType
    {
        /// <summary>
        /// Invited owners
        /// </summary>
        public List<string> OwnerInvitations { get; set; } = new List<string>();
        /// <summary>
        /// Invited moderators
        /// </summary>
        public List<string> ModeratorInvitations { get; set; } = new List<string>();
        /// <summary>
        /// Invited helpers (guests)
        /// </summary>
        public List<string> HelperInvitations { get; set; } = new List<string>();
    }
}