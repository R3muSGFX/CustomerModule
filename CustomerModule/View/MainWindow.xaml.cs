using System;
using System.Windows;
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

        private void BtnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            dataset.AddCustomer(tbCName, tbCSurname, tbCPhone, tbCAddress);
            tbStatusBar.Text = $"Customer {tbCName.Text + tbCSurname.Text}  added to database!";
            ClearValues();
            InitDataset();
        }

        private void ClearValues()
        {
            tbCName.Text = string.Empty;
            tbCSurname.Text = string.Empty;
            tbCPhone.Text = string.Empty;
            tbCAddress.Text = string.Empty;
        }

        private void ResfreshPage_Click(object sender, RoutedEventArgs e)
        {
            InitDataset();
        }

        private void InitDataset()
        {
            dataset = new CustomerDataTable(gridData);
            gridData = dataset.DataGrid;
        }

        private void menuAbout_Click(object sender, RoutedEventArgs e)
        {
            
        }

        #endregion Methods

        #region Properties

        private CustomerDataTable dataset;

        #endregion Properties

        
    }
}
