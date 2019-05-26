using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YourTime.Models
{
    public class Wall
    {
        [BsonElement("Posts")]
        public List<Post> Posts { get; set; }

        [BsonElement("UserId")]
        public string UserId { get; set; }

        public List<string> PostIds { get; set; }

    }
}
