using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace YourTime.Models
{
    public class Circle
    {

        public Circle()
        {
            UsersId = new List<string>();
        }
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("CircleName")]
        public string Circlename { get; set; }

        [BsonElement("CircleUsers")]
        public List<string> UsersId { get; set; }
    }
}
