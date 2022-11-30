using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301203886_daniel_Lab2
{
    [DynamoDBTable("Bookshelf")]
    class Bookshelf
    {
        [DynamoDBHashKey] //Partition key
        public string Username { get; set; }

        [DynamoDBRangeKey]
        public string ISBN { get; set; }

        [DynamoDBProperty("Authors")]
        public List<string> BookAuthors { get; set; }

        public string Edition { get; set; }

        public string Publisher { get; set; }

        public string Title { get; set; }

        public string LastAccess { get; set; }

        public string PageNumber { get; set; }
    }
}
