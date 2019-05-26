using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YourTime.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string UsersName { get; set; }

        [BsonElement("Gender")]
        public string Gender { get; set; }

        [BsonElement("Circles")] 
        public List<string> Circles { get; set; }

        [BsonElement("Age")]
        public int Age { get; set; }

        [BsonElement("Wall")]
        public string WallId { get; set; }

        [BsonElement("Blacklist")]
        public List<string> Blacklist { get; set; }

        [BsonElement("Blacklisted")]
        public List<string> Blacklisted { get; set; }

        [BsonElement("Follows")]
        public List<string> Follows { get; set; }
    }
}
