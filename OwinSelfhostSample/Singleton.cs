using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using MongoDB;

namespace OwinSelfhostSample
{
    public class Singleton
    {
        private static Singleton instance = null;

        public static Singleton Instance
        {
            get { return instance ?? (instance = new Singleton()); }
        }

        public IMongo Mongo { get; private set; }

        public IMongo setMongo(IMongo mongo)
        {
            return Mongo = mongo;
        }

    }
}
