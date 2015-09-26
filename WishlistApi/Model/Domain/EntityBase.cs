using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace WishlistApi.Model.Domain
{
    /// <summary>
    /// Base entity
    /// </summary>
    public class EntityBase: ComplexType
    {
        /// <summary>
        /// document unique identifier
        /// </summary>
        [BsonId]
        public string Id { get; set; }

    }

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