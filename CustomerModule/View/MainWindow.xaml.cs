using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;
using CustomerModule.ViewModel;
using Gat.Controls;

namespace CustomerModule.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        #region Methods

        public MainWindow()
        {
            InitializeComponent();
            InitDataset();
        }

        private void InitDataset()
        {
            dataset = new CustomerDataTable(gridData);
            gridData = dataset.DataGrid;
        }

        private void ClearValues()
        {
            tbCName.Text = string.Empty;
            tbCSurname.Text = string.Empty;
            tbCPhone.Text = string.Empty;
            tbCAddress.Text = string.Empty;
        }

        #region Click methods

        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            dataset.AddCustomer(tbCName, tbCSurname, tbCPhone, tbCAddress);
            tbStatusBar.Text = $"Customer {tbCName.Text + tbCSurname.Text}  added to database!";
            ClearValues();
            InitDataset();
        }
        
        private void ResfreshPage_Click(object sender, RoutedEventArgs e)
        {
            InitDataset();
        }

        private void MenuAbout_Click(object sender, RoutedEventArgs e)
        {
            About about = new About
            {
                Copyright = "Moisi Remus 2017",
                Publisher = "Moisi Remus",
                Title = "Customer manangement",
                Description = "Program created for the basic customers management needs.",
                AdditionalNotes = "This program is saving customer information to better manage information.",
                ApplicationLogo = new BitmapImage(new Uri(@"pack://application:,,,/View/customers_logo.png")),
                PublisherLogo = new BitmapImage(new Uri(@"pack://application:,,,/View/customers_logo.png")),
                Hyperlink = new Uri("http://remusmoisi.azurewebsites.net"),
                HyperlinkText = "http://remusmoisi.azurewebsites.net",
            };
            about.Show();
        }

        #endregion Click methods

        #endregion Methods

        #region Properties

        private CustomerDataTable dataset;

        #endregion Properties

        
    }
}
