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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void TableManager_Click(object sender, RoutedEventArgs e)
        {
            TableManager tableManager = new TableManager();
            tableManager.Show();
            //this.Hide();

        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.Show();
        }
    }

}
