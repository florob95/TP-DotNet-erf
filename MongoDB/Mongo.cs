using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB
{
    public class Mongo : IMongo
    {
        private IMongoCollection<Book> _collection;

        public Mongo(IMongoCollection<Book> collection = null)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("local");
            _collection = collection ?? database.GetCollection<Book>("books");
        }

        public async Task<List<Book>> GetAllBook()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Book> GetBook(string id)
        {
            var objectId = ObjectId.Parse(id);
            return await _collection.Find(book => book._id == objectId).SingleAsync();
        }

        public async void AddBook(Book book)
        {
            await _collection.InsertOneAsync(book);
        }

        public async void DeleteBook(string id)
        {
            var objectId = ObjectId.Parse(id);
            await _collection.DeleteOneAsync(book => book._id == objectId);
        }

        public void UpdateBook(string id, Book bookParam)
        {
            var objectId = ObjectId.Parse(id);
            _collection.UpdateOneAsync(
                book => book._id == objectId,
                Builders<Book>.Update.Set(book => book.name, bookParam.name));
        }
    }
}