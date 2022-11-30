using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using Amazon;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.Model;
using Amazon.S3;
using Amazon.S3.Model;


namespace _301203886_daniel_Lab2
{
    /// <summary>
    /// Interaction logic for PdfView.xaml
    /// </summary>
    public partial class PdfView : Window
    {

        private string awsAccessKey = ConfigurationManager.AppSettings["accessId"];
        private string awsSecretKey = ConfigurationManager.AppSettings["secretKey"];
        private static readonly RegionEndpoint bucketRegion = RegionEndpoint.USEast1;
        private static IAmazonS3 s3Client;

        string getBookPath = @"C:\books\";
        string bucketName = "books-comp306-danielmachado";

        string ISBN;
        string Username;
        List<string> BookAuthors;
        string Edition;
        string Publisher;
        string bookTitle;
        string LastAccess;
        string PageNumber;

        AmazonDynamoDBClient client;
        // Amazon.DynamoDBv2.DataModel.DynamoDBContext context;
        Amazon.Runtime.BasicAWSCredentials credentials;
        
        DynamoDBContext context;

        

        public PdfView(string isbn, string username, List<string> authors, string edition, string publiser, string title, string date, string page)
        {
            InitializeComponent();
            ISBN = isbn;
            Username = username;
            BookAuthors = authors;
            Edition = edition;
            Publisher = publiser;
            bookTitle = title;
            LastAccess = date;
            PageNumber = page;


            Debug.WriteLine(ISBN);
            //pdfViewerControl.Load(@$"C:\Users\DMJS\Desktop\Daniel\Software Eng\COMP306\books\{ISBN}.pdf");
            //pdfViewerControl.Load($"{path}/{ISBN}.pdf");
            //UpdateBookkshelf();

        }

        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Loads the document in PdfViewerControl
            //pdfViewerControl.Load(@"C: \Users\DMJS\Desktop\Academic Transcript_Daniel_Silva.pdf");
            s3Client = new AmazonS3Client(awsAccessKey, awsSecretKey, bucketRegion);
            await DownloadObjectFromBucketAsync(s3Client, bucketName, ISBN, getBookPath);

            
            pdfViewerControl.Load($"{getBookPath}{ISBN}.pdf");
            if(PageNumber != null)
            {
                pdfViewerControl.GoToPageAtIndex(Int32.Parse(PageNumber));
            }
            


        }

        public async Task UpdateBookkshelf(string pageNumber)
        {
            credentials = new Amazon.Runtime.BasicAWSCredentials(ConfigurationManager.AppSettings["accessId"], ConfigurationManager.AppSettings["secretKey"]);
            client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            context = new DynamoDBContext(client);
            Dictionary<string, AttributeValue> key = new Dictionary<string, AttributeValue>
            {
                { "Username", new AttributeValue { S = Username } },
                { "ISBN", new AttributeValue { S = ISBN } }
            };

            // Define attribute updates
            Dictionary<string, AttributeValueUpdate> updates = new Dictionary<string, AttributeValueUpdate>();
            // Update item's Setting attribute
            string newDate = DateTime.Now.ToString();
            updates["LastAccess"] = new AttributeValueUpdate()
            {
                Action = AttributeAction.PUT,
                Value = new AttributeValue { S = newDate }
            };

            updates["PageNumber"] = new AttributeValueUpdate()
            {
                Action = AttributeAction.PUT,
                Value = new AttributeValue { S = pageNumber }
            };


            // Create UpdateItem request
            UpdateItemRequest request = new UpdateItemRequest
            {
                TableName = "Bookshelf",
                Key = key,
                AttributeUpdates = updates
            };

            // Issue request
            await client.UpdateItemAsync(request);

        }

        public static async Task<bool> DownloadObjectFromBucketAsync(
            IAmazonS3 client,
            string bucketName,
            string objectName,
            string filePath)
        {
            // Create a GetObject request
            var request = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = $"{objectName}.pdf",
            };

            // Issue request and remember to dispose of the response
            using GetObjectResponse response = await client.GetObjectAsync(request);

            try
            {
                // Save object to local file
                await response.WriteResponseStreamToFileAsync($"{filePath}\\{objectName}.pdf", true, System.Threading.CancellationToken.None);
                return response.HttpStatusCode == System.Net.HttpStatusCode.OK;
            }
            catch (AmazonS3Exception ex)
            {
                Console.WriteLine($"Error saving {objectName}: {ex.Message}");
                return false;
            }
        }

        void pdfviewer1_CurrentPageChanged(object sender, EventArgs args)
        {
            PageNumber = pdfViewerControl.CurrentPageIndex.ToString();
        }

        private async void Window_Closing(object sender, CancelEventArgs e)
        {
        
            await UpdateBookkshelf(PageNumber);
            BookshelfList bookshelfList = new BookshelfList(Username);
            bookshelfList.Show();
            
        }
    }
}
