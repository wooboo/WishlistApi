using MongoDB.Bson.Serialization.Attributes;

namespace WishlistApi.Model.Domain
{
    /// <summary>
    /// Base entity
    /// </summary>
    public class EntityBase: ComplexType, IIdentified
    {
        /// <summary>
        /// document unique identifier
        /// </summary>
        [BsonId]
        public string Id { get; set; }

    }
}