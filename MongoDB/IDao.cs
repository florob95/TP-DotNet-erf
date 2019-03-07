using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MongoDB
{
    public interface IDao<T> : IDisposable
    {
        Task<List<T>> GetAll();
        Task<T> Get(string id);
        void Add(T item);
        void Delete(string id);
        void Update(string id, T item);
    }
}