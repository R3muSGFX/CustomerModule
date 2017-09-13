using System.ComponentModel.DataAnnotations;

namespace CustomerModule.Model
{
    public class Customer
    {
        #region Methods

        public Customer() { }

        public Customer(
            int customerId, 
            string customerName, 
            string customerSurname, 
            string customerPhonenumber, 
            string customerAddress)
        {
            this.customerId = customerId;
            this.customerName = customerName;
            this.customerSurname = customerSurname;
            this.customerPhonenumber = customerPhonenumber;
            this.customerAddress = customerAddress;
        }

        public override bool Equals(object obj)
        {
            var customer = obj as Customer;
            return customer != null &&
                   customerId == customer.customerId &&
                   customerName == customer.customerName &&
                   customerSurname == customer.customerSurname &&
                   customerPhonenumber == customer.customerPhonenumber &&
                   customerAddress == customer.customerAddress;
        }

        public override string ToString()
        {
            return $"Customer ID: {customerId} " +
                   $"Customer full name: {customerName} {customerSurname} " +
                   $"Customer phone: {customerPhonenumber} " +
                   $"Customer address: {customerAddress}";
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        #endregion Methods

        #region Properties

        private int customerId;
        private string customerName;
        private string customerSurname;
        private string customerPhonenumber;
        private string customerAddress;
        
        
        public int CustomerId { get => customerId; set => customerId = value; }

        [StringLength(30)]
        public string CustomerName { get => customerName; set => customerName = value; }

        [StringLength(30)]
        public string CustomerSurname { get => customerSurname; set => customerSurname = value; }

        [StringLength(10)]
        public string CustomerPhonenumber { get => customerPhonenumber; set => customerPhonenumber = value; }

        [StringLength(50)]
        public string CustomerAddress { get => customerAddress; set => customerAddress = value; }
        
        #endregion Properties
    }
}
