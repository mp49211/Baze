using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Baze_NoSQL.Models
{
    public class Komentar
    {
        [BsonElement("vrijeme")]
        public DateTime Vrijeme { get; set; }
        [BsonElement("tekst")]
        public string Tekst { get; set; }
    }
}
