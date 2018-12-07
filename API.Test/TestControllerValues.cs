using System.Web.Http;
using Model;
using MongoDB;
using NSubstitute;
using NUnit.Framework;
using OwinSelfhostSample;

namespace API.Test
{
    [TestFixture]
    public class TestControllerValues
    {
        private ValuesController _controller;
        private IMongo _mongo;
        private Book _book;

        [SetUp]
        public void SetUp()
        {
            _controller = new ValuesController();
            _mongo = Singleton.Instance.setMongo(Substitute.For<IMongo>());
            _book = new Book {name = "test"};
        }

        [TearDown]
        public void TearDown()
        {
            _mongo.ClearReceivedCalls();
        }

        [Test]
        public void GetAll()
        {
            _controller.Get();
            _mongo.Received().GetAllBook();
            Assert.IsInstanceOf<IHttpActionResult>(_controller.Get());
        }

        [Test]
        public void Get()
        {
            _controller.Get("5c06f4b43cd1d72a48b44237");
            _mongo.Received().GetBook(Arg.Any<string>());
            Assert.IsInstanceOf<IHttpActionResult>(_controller.Get(Arg.Any<string>()));
        }

        [Test]
        public void Post()
        {
            _controller.Post(_book);
            _mongo.Received().AddBook(Arg.Any<Book>());
            Assert.IsInstanceOf<IHttpActionResult>(_controller.Post(Arg.Any<Book>()));
        }

        [Test]
        public void Put()
        {
            _controller.Put("5c06f4b43cd1d72a48b44237", _book);
            _mongo.Received().UpdateBook(Arg.Any<string>(), Arg.Any<Book>());
            Assert.IsInstanceOf<IHttpActionResult>(_controller.Put(Arg.Any<string>(), Arg.Any<Book>()));
        }

        [Test]
        public void Delete()
        {
            _controller.Delete("5c06f4b43cd1d72a48b44237");
            _mongo.Received().DeleteBook(Arg.Any<string>());
            Assert.IsInstanceOf<IHttpActionResult>(_controller.Delete(Arg.Any<string>()));
        }
    }
}