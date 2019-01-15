using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Baze_NoSQL.Models
{
    public class Baza
    {
        MongoClient _client;
        MongoServer _server;
        MongoDatabase _db;
        private static IMongoClient client = new MongoClient();
        private static IMongoDatabase database = client.GetDatabase("portal");
        public Baza()
        {
            _client = new MongoClient("mongodb://localhost:27017");
            _server = _client.GetServer();
            _db = _server.GetDatabase("portal");
        }

        public IEnumerable<Clanak> GetClanci()
        {
            return _db.GetCollection<Clanak>("clanci").FindAll();
        }
        public IEnumerable<Clanak> GetClanci(int n)
        {
            var sort = SortBy.Descending("datum");

            return _db.GetCollection<Clanak>("clanci").FindAll().SetSortOrder(sort).Take(n);
        }

        public Clanak GetClanak(ObjectId id)
        {
            var res = Query<Clanak>.EQ(p => p.Id, id);
            return _db.GetCollection<Clanak>("clanci").FindOne(res);
        }

        public Clanak Create(Clanak p)
        {
            _db.GetCollection<Clanak>("clanci").Save(p);
            return p;
        }
        public void Update(ObjectId id, Clanak p)
        {
            p.Id = id;
            var res = Query<Clanak>.EQ(pd => pd.Id, id);
            var operation = Update<Clanak>.Replace(p);
            _db.GetCollection<Clanak>("clanci").Update(res, operation);
        }
        public void Remove(ObjectId id)
        {
            var res = Query<Clanak>.EQ(e => e.Id, id);
            var operation = _db.GetCollection<Clanak>("clanci").Remove(res);
        }

        public byte[] GetSlika(ObjectId id)
        {
            var file = _db.GridFS.FindOne(Query.EQ("_id", id));
            
            using (var stream = file.OpenRead())
            {
                var bytes = new byte[stream.Length];
                stream.Read(bytes, 0, (int)stream.Length);
                return bytes;
            }
        }

        public void AddKomentar (string komentar, ObjectId id)
        {
            var kom = new Komentar { Vrijeme = DateTime.Now, Tekst = komentar };
            var collection = database.GetCollection<Clanak>("clanci");
            var filter = Builders<Clanak>.Filter.Eq("_id", id);
            var update = Builders<Clanak>.Update.Push("komentari", kom);
            collection.UpdateOne(filter, update);
        }
    }
}
