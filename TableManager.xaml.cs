using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;
using System.Configuration;
using Amazon;
using Amazon.DynamoDBv2.DocumentModel;

namespace _301203886_daniel_Lab2
{
    /// <summary>
    /// Interaction logic for TableManager.xaml
    /// </summary>
    public partial class TableManager : Window
    {
        public TableManager()
        {
            InitializeComponent();
        }

        private AmazonDynamoDBClient client;
        private BasicAWSCredentials credentials;
        private RegionEndpoint region = RegionEndpoint.USEast1;
        private string newTableName;



        public async Task CreateTable()
        {
            credentials = new BasicAWSCredentials(
                                 ConfigurationManager.AppSettings["accessId"],
                                 ConfigurationManager.AppSettings["secretKey"]);
            client = new AmazonDynamoDBClient(credentials, region);

            CreateTableRequest request = new CreateTableRequest
            {
                TableName = newTableName,
                AttributeDefinitions = new List<AttributeDefinition>()
                {
                    new AttributeDefinition
                    {
                        AttributeName="Username",
                        AttributeType="S"
                    },
                     new AttributeDefinition
                    {
                        AttributeName="Password",
                        AttributeType="S"
                    }
                },
                KeySchema = new List<KeySchemaElement>()
                {
                    new KeySchemaElement
                    {
                        AttributeName="Username",
                        KeyType="HASH"
                    },
                    new KeySchemaElement
                    {
                        AttributeName="Password",
                        KeyType="RANGE"
                    }
                },
                BillingMode = BillingMode.PROVISIONED,
                ProvisionedThroughput = new ProvisionedThroughput
                {
                    ReadCapacityUnits = 2,
                    WriteCapacityUnits = 1
                }
            };

            try
            {
                var response = await client.CreateTableAsync(request);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK) Console.WriteLine("Table created successfully");

            }
            catch (InternalServerErrorException iee)
            {
                Console.WriteLine("An error occurred on the server side " + iee.Message);
            }
            catch (LimitExceededException lee)
            {
                Console.WriteLine("you are creating a table with one or more secondary indexes+ " + lee.Message);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            newTableName = tableName.Text;
            await CreateTable();

        }

        private async void Insert_User_Click(object sender, RoutedEventArgs e)
        {
            newTableName = tableName.Text;

            DDBOperation op = new DDBOperation(newTableName);
            string username = newUserName.Text;
            string password = newPassword.Text;
            await op.InsertAsync(username, password);
        }
    }
}
