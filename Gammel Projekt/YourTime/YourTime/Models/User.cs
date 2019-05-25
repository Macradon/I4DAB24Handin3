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
        public string Name { get; set; }

        [BsonElement("Age")]
        public string Age { get; set; }

        [BsonElement("Gender")]
        public string Gender { get; set; }

        [BsonElement("CircleIDs")]
        public string CircleIDs { get; set; }

        [BsonElement("Follow")]
        public string Follow { get; set; }

        [BsonElement("Blacklist")]
        public string Blacklist { get; set; }
    }
}
