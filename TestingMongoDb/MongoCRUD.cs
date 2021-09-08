using System;
using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using TestingMongoDb.models;

namespace TestingMongoDb
{
    public class MongoCRUD
    {
        private IMongoDatabase db;

        public MongoCRUD(string database)
        {
            Connection(database);
        }

        public IMongoDatabase GetClient()
        {
            return db;
        }

        public List<string> GetIds<T>(string collectionName) where T : MongoEntity
        {
            var records = LoadRecords<T>(collectionName);
            var selected = records.Select(x => x.Id).ToList();
            return selected;
        }
        
        public void Connection(string database)
        {
            //var set = new MongoClientSettings();
            //set.ConnectTimeout = new TimeSpan(0, 12, 0);
            //set.)

            var client = new MongoClient("mongodb+srv://doncho-patronski:0J5009LicwxF5o0Z@zzg-dev-cluster.fqser.azure.mongodb.net/test?retryWrites=true&w=majority&connect=replicaSet&connectTimeoutMS=300000");
            db = client.GetDatabase(database);
        }

        public void InsertRecord<T>(string collectionName, T record)
        {
            var collection = db.GetCollection<T>(collectionName);
            collection.InsertOne(record);
        }

        public List<T> LoadRecords<T>(string collectionName)
        {
            var collection = db.GetCollection<T>(collectionName);

            return collection.Find(new BsonDocument()).ToList();
        }

        public T LoadRecordById<T>(string collectionName, string id)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);

            return collection.Find(filter).First();
        }

        public void UpsertRecord<T>(string collectionName, string id, T record)
        {
            var collection = db.GetCollection<T>(collectionName);

            //var result = collection.ReplaceOne(
            //    new BsonDocument("_id", id),
            //    record,
            //    new ReplaceOptions { IsUpsert = true });
        }

        public void DeleteRecord<T>(string collectionName, string id)
        {
            var collection = db.GetCollection<T>(collectionName);
            var filter = Builders<T>.Filter.Eq("Id", id);

            collection.DeleteOne(filter);
        }
    }
}
