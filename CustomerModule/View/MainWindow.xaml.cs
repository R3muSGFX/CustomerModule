using System.Windows;
using CustomerModule.Model;
using System.Collections.Generic;

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
            database = new ObjectAdapter();
            customers = database.SelectCustomers();
            gridData.ItemsSource = customers;
        }

        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = new Customer
            {
                CustomerId = 0,
                CustomerName = tbCName.Text,
                CustomerSurname = tbCSurname.Text,
                CustomerPhonenumber = tbCPhone.Text,
                CustomerAddress = tbCAddress.Text
            };

            database.InsertCustomer(customer);
            gridData.ItemsSource = customersDataTable.UpdateDataTable();

            tbCName.Text = tbCSurname.Text = tbCPhone.Text = tbCAddress.Text = "";
            tbStatusBar.Text = "Customer added to database!";
        }

        #endregion Methods

        #region Properties

        private ObjectAdapter database;
        private List<Customer> customers;

        #endregion Properties

    }
}
