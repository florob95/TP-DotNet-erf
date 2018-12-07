using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace Model
{
    public class Book
    {
        public ObjectId _id { get; set; }
        public int number { get; set; }
        public string name { get; set; }
    }
}
