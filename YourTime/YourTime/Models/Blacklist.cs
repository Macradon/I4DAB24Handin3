using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YourTime.Models
{
    public class Blacklist
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("Banlist")]
        public List<string> BannedList { get; set; }
    }
}
