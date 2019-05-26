using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YourTime.Models
{
    public class Post
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("User")]
        public string UserID { get; set; }

        [BsonElement("Content")]
        public string Content { get; set; }

        [BsonElement("Privacy")]
        public string Privacy { get; set; }
    }
}
