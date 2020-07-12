using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;

namespace AdvancedMongo
{
    public class Student
    {
        public ObjectId _id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string @class { get; set; }
        public int age { get; set; }
        public IEnumerable<string> subjects { get; set; }
    }
}
