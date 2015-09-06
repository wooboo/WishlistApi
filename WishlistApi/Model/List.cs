using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace WishlistApi.Model
{
    public class EntityBase
    {
        [BsonId]
        public string Id { get; set; }

    }
    public class List:EntityBase
    {
        
        public string Name { get; set; }
        public List<Wish> Wishes { get; set; } = new List<Wish>();
        public List<string> Owners { get; set; } = new List<string>();
        public List<string> Moderators { get; set; } = new List<string>();
        public ListInvitations Invitations { get; set; } = new ListInvitations();
    }
}
