using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Model;
using MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;
using NSubstitute;
using NUnit.Framework;
using OwinSelfhostSample;

namespace API.Test
{
    [TestFixture]
    public class TestMongo
    {
        private Book _book;
        private IMongoCollection<Book> _collection;
        private IMongo _mongo;

        [SetUp]
        public void SetUp()
        {
            _collection = Substitute.For<IMongoCollection<Book>>();
            _mongo = new Mongo(_collection);
            _book = new Book {name = "test", number = 1, _id = new ObjectId("5c06f4b43cd1d72a48b44237")};
        }

        [TearDown]
        public void TearDown()
        {
            _collection.ClearReceivedCalls();
        }

        [Test]
        public void TestGetAllBook()
        {
            _mongo.GetAllBook();

            _collection.Received().Find(new BsonDocument());
            Assert.IsInstanceOf<Task<List<Book>>>(_mongo.GetAllBook())
        }

        [Test]
        public void TestGetBook()
        {
            _mongo.GetBook("5c06f4b43cd1d72a48b44237");
            _collection.Received().Find(book => book._id == Arg.Any<ObjectId>());
            Assert.IsInstanceOf<Task<Book>>(_mongo.GetBook(Arg.Any<string>()));
        }

        [Test]
        public void TestAdd()
        {
            _mongo.AddBook(_book);
            _collection.Received().InsertOneAsync(Arg.Any<Book>());
        }

        [Test]
        public void TestDelete()
        {
            _mongo.DeleteBook("5c06f4b43cd1d72a48b44237");
            _collection.Received()
                .DeleteOneAsync(Arg.Any<ExpressionFilterDefinition<Book>>(), Arg.Any<CancellationToken>());
        }

        [Test]
        public void Test()
        {
            _mongo.UpdateBook("5c06f4b43cd1d72a48b44237", _book);
            _collection.Received().UpdateOneAsync(Arg.Any<ExpressionFilterDefinition<Book>>(),
                Arg.Any<UpdateDefinition<Book>>()
            );
        }
    }
}