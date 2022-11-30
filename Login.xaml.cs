using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace _301203886_daniel_Lab2
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        AmazonDynamoDBClient client;
        BasicAWSCredentials credentials;
        string tableName = "User";

        public Login()
        {
            InitializeComponent();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            // LoginOperation newLogin = new LoginOperation();
            //await newLogin.Login(usernameLogin.Text);
            await GetItem();

        }
        public void SaveUsername()
        {
            string UserAuth = usernameLogin.Text;
        }

        public async Task GetItem()
        {
            credentials = new BasicAWSCredentials(
                                ConfigurationManager.AppSettings["accessId"],
                                ConfigurationManager.AppSettings["secretKey"]);

            client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);

            GetItemRequest request = new GetItemRequest
            {
                TableName = tableName,
                Key = new Dictionary<string, AttributeValue>
                {
                    { "Username", new AttributeValue{ S= usernameLogin.Text} },
                    { "Password", new AttributeValue{ S= passwordLogin.Text } }
                }
            };

            try
            {
                var response = await client.GetItemAsync(request);
                if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
                {
                    if (response.Item.Count > 0)
                    {
                        BookshelfList bookshelfList = new BookshelfList(usernameLogin.Text);
                        bookshelfList.Show();
                        this.Hide();
                        /*
                        Console.WriteLine("Item(s) retrieved successfully");
                        foreach (var item in response.Item)
                            Console.WriteLine($"Key: {item.Key}, Value: {item.Value.S}{item.Value.N}");
                        */
                    }
                }
            }
            catch (InternalServerErrorException iee)
            { Console.WriteLine("An error occurred on the server side " + iee.Message); }

            catch (ResourceNotFoundException ex)
            { Console.WriteLine("The operation tried to access a nonexistent table or index. " + ex.Message); }
        }
    }
}
