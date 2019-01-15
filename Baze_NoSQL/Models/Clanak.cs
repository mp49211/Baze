using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
//using static System.Net.Mime.MediaTypeNames;

namespace Baze_NoSQL.Models
{
    public class Clanak
    {
        public ObjectId Id { get; set; }
        [BsonElement("autor")]
        public string Autor { get; set; }
        [BsonElement("naslov")]
        public string Naslov { get; set; }
        [BsonElement("tekst")]
        public string Tekst { get; set; }
        [BsonElement("datum")]
        public DateTime Datum { get; set; }
        [BsonElement("slika_id")]
        public ObjectId SlikaId { get; set; }
        public byte[] Slika { get; set; }
        public string SlikaStr { get; set; }
        [BsonElement("komentari")]
        public List<Komentar> Komentari { get; set; }
    }
}
