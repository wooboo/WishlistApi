using System.Collections.Generic;

namespace WishlistApi.Model.Domain
{
    /// <summary>
    /// Base complex type
    /// </summary>
    public class ComplexType
    {
        /// <summary>
        /// All extra elements from store that are not in model
        /// </summary>
        public IDictionary<string, object> ExtraElements { get; set; } 
    }
}