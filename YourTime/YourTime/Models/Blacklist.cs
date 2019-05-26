using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson.Serialization.Attributes;

namespace YourTime.Models
{
    public class Blacklist
    {
        [BsonElement("UserId")]
        public string UserId { get; set; }

        [BsonElement("Banlist")]
        public List<string> BannedList { get; set; }
    }
}
