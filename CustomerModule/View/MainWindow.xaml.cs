using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using CostumerModule.View.AboutBox;
using CustomerModule.Model;
using CustomerModule.ViewModel;

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

        private void ClearUpdateDeleteValues()
        {
            tbUpdateName.Text = string.Empty;
            tbUpdateSurname.Text = string.Empty;
            tbUpdatePhone.Text = string.Empty;
            tbUpdateAddress.Text = string.Empty;
            gridUpdateDelete.IsEnabled = false;
        }

        private void UpdateDeleteFill(Customer customer)
        {
            tbUpdateName.Text = customer.CustomerName;
            tbUpdateSurname.Text = customer.CustomerSurname;
            tbUpdatePhone.Text = customer.CustomerPhonenumber;
            tbUpdateAddress.Text = customer.CustomerAddress;
            customerRowId = customer.CustomerId;
            gridUpdateDelete.IsEnabled = true;
        }

        #region Click methods

        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            dataset.AddCustomer(tbCName, tbCSurname, tbCPhone, tbCAddress);
            tbStatusBar.Text = $"Customer {tbCName.Text} {tbCSurname.Text}  added to database!";
            ClearValues();
            InitDataset();
        }
        
        private void ResfreshPage_Click(object sender, RoutedEventArgs e)
        {
            InitDataset();
            ClearValues();
            ClearUpdateDeleteValues();
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

        private void Row_DoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            gridUpdateDelete.IsEnabled = true;
            DataGridRow row = sender as DataGridRow;
            Customer customer = row.Item as Customer;
            UpdateDeleteFill(customer);
        }

        private void BtnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            dataset.DeleteCustomer(customerRowId);
            tbStatusBar.Text = $"Customer {tbUpdateName.Text} {tbUpdateSurname.Text} deleted from database!";
            ClearUpdateDeleteValues();
            InitDataset();
        }

        private void BtnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            dataset.UpdateCustomer(customerRowId, tbUpdateName, tbUpdateSurname, tbUpdatePhone, tbUpdateAddress);
            tbStatusBar.Text = $"Customer {tbUpdateName.Text} {tbUpdateSurname.Text} updated in database!";
            ClearUpdateDeleteValues();
            InitDataset();
        }

        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?",
                                "Exit", MessageBoxButton.YesNo,
                                MessageBoxImage.Question)
                               == MessageBoxResult.Yes)
            {
                base.OnClosing(e);
            }
            else e.Cancel = true;
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e) => Application.Current.Shutdown();

        private void MenuHelp_Click(object sender, RoutedEventArgs e)
        {
            //TODO: how to use
        }

        #endregion Click methods

        #endregion Methods

        #region Properties

        private CustomerDataTable dataset;
        private int customerRowId;

        #endregion Properties

    }
}