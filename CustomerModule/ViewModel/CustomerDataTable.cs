using System.Collections.Generic;
using System.Windows.Controls;
using CustomerModule.Model;

namespace CustomerModule.ViewModel
{
    public class CustomerDataTable : DataGrid
    {

        public CustomerDataTable(DataGrid dataGrid)
        {
            this.dataGrid = dataGrid;
            database = new ObjectAdapter();
            customers = database.SelectCustomers();
            dataGrid.ItemsSource = customers;
        }

        public void UpdateTable()
        {
            customers = database.SelectCustomers();
        }

        public void AddCustomer(TextBox tbName, TextBox tbSurname, TextBox tbPhone, TextBox tbAddress)
        {
            Customer customer = new Customer()
            {
                CustomerId = 0,
                CustomerName = tbName.Text,
                CustomerSurname = tbSurname.Text,
                CustomerPhonenumber = tbPhone.Text,
                CustomerAddress = tbAddress.Text
            };
            database.InsertCustomer(customer);
            UpdateTable();
        }

        public void UpdateCustomer(int id, TextBox tbName, TextBox tbSurname, TextBox tbPhone, TextBox tbAddress)
        {
            Customer customer = new Customer()
            {
                CustomerId = id,
                CustomerName = tbName.Text,
                CustomerSurname = tbSurname.Text,
                CustomerPhonenumber = tbPhone.Text,
                CustomerAddress = tbAddress.Text
            };
            database.UpdateCustomer(customer);
            UpdateTable();
        }

        public void DeleteCustomer(int id)
        {
            database.DeleteCustomer(id);
            UpdateTable();
        }

        private ObjectAdapter database;
        private List<Customer> customers;
        private DataGrid dataGrid;

        public DataGrid DataGrid { get => dataGrid; set => dataGrid = value; }
    }
}
