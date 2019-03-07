using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MongoDB.Driver.Core.Operations;

namespace MongoDB
{
    public class Factory
    {
        public static IDao<T> Create<T>()
        {
            if (typeof(T) == typeof(Book))
            {
                var dao = new Mongo() as IDao<Book>;
                var logProxy = new LogProxy<IDao<Book>>(dao);
                return logProxy.GetTransparentProxy() as IDao<T>;
            }

            return null;
        }
    }
}