using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.Runtime;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _301203886_daniel_Lab2
{
    public class DDBOperation
    {
        AmazonDynamoDBClient client;
        // Amazon.DynamoDBv2.DataModel.DynamoDBContext context;
        Amazon.Runtime.BasicAWSCredentials credentials;

     

        Table userTable;

        DynamoDBContext context;

        public DDBOperation(string tableName)
        {
            credentials = new Amazon.Runtime.BasicAWSCredentials(ConfigurationManager.AppSettings["accessId"], ConfigurationManager.AppSettings["secretKey"]);
            client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            userTable = Table.LoadTable(client, tableName, DynamoDBEntryConversion.V2); //load the metadata of the table
        }


        public DDBOperation()
        {
            credentials = new Amazon.Runtime.BasicAWSCredentials(ConfigurationManager.AppSettings["accessId"], ConfigurationManager.AppSettings["secretKey"]);
            client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            context = new DynamoDBContext(client);
        }

        public async Task InsertAsync(string username, string password)
        {
            Document newUser = new Document();
            newUser["Username"] = username;//newUserName.Text;
            newUser["Password"] = password;//newPassword.Text;

            await userTable.PutItemAsync(newUser);
        }

   




    }
}
