using System.Collections.Generic;
using System.Threading.Tasks;
using Model;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDB
{
    public class Mongo : IDao<Book>
    {
        private IMongoCollection<Book> _collection;

        public Mongo(IMongoCollection<Book> collection = null)
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("local");
            _collection = collection ?? database.GetCollection<Book>("books");
        }

        public void Dispose()
        {
            _collection = null;
        }

        public async Task<List<Book>> GetAll()
        {
            return await _collection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<Book> Get(string id)
        {
            var objectId = ObjectId.Parse(id);
            return await _collection.Find(book => book._id == objectId).SingleAsync();
        }

        public async void Add(Book book)
        {
            await _collection.InsertOneAsync(book);
        }

        public async void Delete(string id)
        {
            var objectId = ObjectId.Parse(id);
            await _collection.DeleteOneAsync(book => book._id == objectId);
        }

        public void Update(string id, Book bookParam)
        {
            var objectId = ObjectId.Parse(id);
            _collection.UpdateOneAsync(
                book => book._id == objectId,
                Builders<Book>.Update.Set(book => book.name, bookParam.name));
        }
    }
}