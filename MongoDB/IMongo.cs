using System.Collections.Generic;
using System.Threading.Tasks;
using Model;

namespace MongoDB
{
    public interface IMongo
    {
        Task<List<Book>> GetAllBook();
        Task<Book> GetBook(string id);
        void AddBook(Book book);
        void DeleteBook(string id);
        void UpdateBook(string id, Book book);
    }
}