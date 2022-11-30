using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using Amazon.Runtime;

namespace _301203886_daniel_Lab2
{
    /// <summary>
    /// Interaction logic for BookshelfList.xaml
    /// </summary>
    public partial class BookshelfList : Window
    {
        
        AmazonDynamoDBClient client;
        BasicAWSCredentials credentials;
        //string tableName = "Bookshelf";
        string userName = "";
        string ISBNselected;

        string strISBN = "";
        string strUser = "";
        List<string> strAuthor;
        string strEdition = "";
        string strPublisher = "";
        string strTitle = "";
        string strLastAccess = "";
        string strPageNum = "";

        public object ISBN { get; private set; }

        public BookshelfList(string name)
        {
            userName = name;
            InitializeComponent();
            RunList();
        }

        public async void RunList()
        {
            await GetList();
        }

         

        public async Task GetList()
        {
            credentials = new Amazon.Runtime.BasicAWSCredentials(ConfigurationManager.AppSettings["accessId"], ConfigurationManager.AppSettings["secretKey"]);
            client = new AmazonDynamoDBClient(credentials, Amazon.RegionEndpoint.USEast1);
            DynamoDBContext context = new DynamoDBContext(client);

            List<Bookshelf> result = await context.ScanAsync<Bookshelf>(new List<ScanCondition>()).GetRemainingAsync();

            foreach (Bookshelf s in result)
                Console.WriteLine(s);


             var config = new DynamoDBOperationConfig
              {
                  QueryFilter = new List<ScanCondition>
                  { new ScanCondition("Username", ScanOperator.Equal,userName)}
              };
            /* 
            var config = new DynamoDBOperationConfig
            {
                IndexName = "ProgramCode-index"
            };*/

            result = await context.QueryAsync<Bookshelf>(userName, config).GetRemainingAsync();
            List<Bookshelf> bookshelf = new List<Bookshelf>();
           
            foreach (Bookshelf b in result)
            {
                


                bookshelf.Add(new Bookshelf() { Title = b.Title, ISBN = b.ISBN, Publisher = b.Publisher, Edition = b.Edition, BookAuthors = b.BookAuthors, Username = b.Username, LastAccess = b.LastAccess, PageNumber = b.PageNumber });
                Debug.WriteLine(b.Username);
            }

            booklistDG.ItemsSource = bookshelf;

            CollectionView view = (CollectionView)CollectionViewSource.GetDefaultView(booklistDG.ItemsSource);
            view.SortDescriptions.Add(new SortDescription("LastAccess", ListSortDirection.Descending));



        }
        private void dataGrid_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

          
            

            if (booklistDG.SelectedItems.Count > 0)
            {
                Bookshelf book = new Bookshelf();
                foreach (var obj in booklistDG.SelectedItems)
                {
                    book = obj as Bookshelf;
                    strISBN = book.ISBN;
                    strUser = book.Username;
                    strAuthor = book.BookAuthors;
                    strEdition = book.Edition;
                    strPublisher = book.Publisher;
                    strTitle = book.Title;
                    strLastAccess = book.LastAccess;
                    strPageNum = book.PageNumber;



                }
            }
             newLabel.Content = strISBN;
            ISBNselected = strISBN;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            PdfView pdfView = new PdfView(ISBNselected, strUser, strAuthor, strEdition, strPublisher, strTitle, strLastAccess, strPageNum); //string isbn, string username, List<string> authors, string edition, string publiser string title
            pdfView.Show();
            this.Close();

        }
    }
}
