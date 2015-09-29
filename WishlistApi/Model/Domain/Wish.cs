using System.Collections.Generic;

namespace WishlistApi.Model.Domain
{
    /// <summary>
    /// Wish
    /// </summary>
    public class Wish: ComplexType, IIdentified
    {
        /// <summary>
        /// Identifier of a wish
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Name of a wish
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Image links
        /// </summary>
        public List<Link> Images { get; set; } = new List<Link>();
        /// <summary>
        /// Links to products
        /// </summary>
        public List<Link> Urls { get; set; } = new List<Link>();
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
    }

    /// <summary>
    /// Link definition
    /// </summary>
    public class Link: ComplexType
    {
        /// <summary>
        /// Title
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
    }
}