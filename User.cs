using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.DynamoDBv2.DataModel;

namespace _301203886_daniel_Lab2
{
    [DynamoDBTable("User")]
    class User
    {
        [DynamoDBHashKey("Username")] //Partition key
        public string Username { get; set; }

        [DynamoDBRangeKey("Password")]
        public int Password { get; set; }
    }
}
