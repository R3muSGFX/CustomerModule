using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CustomerModule.Model
{
    public class ObjectAdapter
    {
        
        #region Methods

        public ObjectAdapter()
        {
            connectionString = Properties.Resources.LocalDB;
            currentConnection = new SqlConnection(connectionString);
        }

        public Customer GetCustomer(Int32? customerId)
        {
            Customer customer = new Customer();
            Int32? errorNumber = default(Int32?);
            String errorMessage = default(String);

            using (currentConnection)
            {
                using (SqlCommand command = new SqlCommand())
                {
                    currentConnection.Open();
                    command.Connection = currentConnection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 1200;
                    command.CommandText = @"[dbo].[Customers_Get]";
                    command.Parameters.Add(new SqlParameter(@"@ERROR_NUMBER", SqlDbType.Int, 4, ParameterDirection.InputOutput, true, 10, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                    command.Parameters.Add(new SqlParameter(@"@ERROR_MESSAGE", SqlDbType.NVarChar, 4000, ParameterDirection.InputOutput, true, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                    command.Parameters.Add(new SqlParameter(@"@CUSTOMER_ID", SqlDbType.Int, 4, ParameterDirection.Input, false, 10, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                    SetParameterValue(command.Parameters[0], errorNumber);
                    SetParameterValue(command.Parameters[1], errorMessage);
                    SetParameterValue(command.Parameters[2], customerId);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            customer.CustomerId = reader.GetInt32(reader.GetOrdinal(@"CUSTOMER_ID"));
                            customer.CustomerName = reader.GetString(reader.GetOrdinal(@"CUSTOMER_NAME"));
                            customer.CustomerSurname = reader.GetString(reader.GetOrdinal(@"CUSTOMER_SURNAME"));
                            customer.CustomerPhonenumber = reader.GetString(reader.GetOrdinal(@"CUSTOMER_PHONENUMBER"));
                            customer.CustomerAddress = reader.GetString(reader.GetOrdinal(@"CUSTOMER_ADDRESS"));
                        }
                    }

                    errorNumber = (Int32?)GetParameterValue(command.Parameters[0]);
                    errorMessage = (String)GetParameterValue(command.Parameters[1]);
                    HandleErrors(errorNumber, errorMessage);
                }
                currentConnection.Close();
            }
            return customer;
        }

        public List<Customer> SelectCustomers()
        {
            List<Customer> customers = new List<Customer>();
            Int32? errorNumber = default(Int32?);
            String errorMessage = default(String);

            using (currentConnection)
            {
                currentConnection.ConnectionString = connectionString;
                using (SqlCommand command = new SqlCommand())
                {
                    currentConnection.Open();
                    command.Connection = currentConnection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandTimeout = 1200;
                    command.CommandText = @"[dbo].[Customers_Select]";
                    command.Parameters.Add(new SqlParameter(@"@ERROR_NUMBER", SqlDbType.Int, 4, ParameterDirection.InputOutput, true, 10, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                    command.Parameters.Add(new SqlParameter(@"@ERROR_MESSAGE", SqlDbType.NVarChar, 4000, ParameterDirection.InputOutput, true, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                    SetParameterValue(command.Parameters[0], errorNumber);
                    SetParameterValue(command.Parameters[1], errorMessage);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Customer customer = new Customer();
                            customer.CustomerId = reader.GetInt32(reader.GetOrdinal(@"CUSTOMER_ID"));
                            customer.CustomerName = reader.GetString(reader.GetOrdinal(@"CUSTOMER_NAME"));
                            customer.CustomerSurname = reader.GetString(reader.GetOrdinal(@"CUSTOMER_SURNAME"));
                            customer.CustomerPhonenumber = reader.GetString(reader.GetOrdinal(@"CUSTOMER_PHONENUMBER"));
                            customer.CustomerAddress = reader.GetString(reader.GetOrdinal(@"CUSTOMER_ADDRESS"));
                            customers.Add(customer);
                        }
                    }

                    errorNumber = (Int32?)GetParameterValue(command.Parameters[0]);
                    errorMessage = (String)GetParameterValue(command.Parameters[1]);
                    HandleErrors(errorNumber, errorMessage);
                }
            }
            return customers;
        }

        public void InsertCustomer(Customer customer)
        {
            Int32? errorNumber = default(Int32?);
            String errorMessage = default(String);

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 1200;
                command.CommandText = @"[dbo].[Customers_Insert]";
                command.Parameters.Add(new SqlParameter(@"@ERROR_NUMBER", SqlDbType.Int, 4, ParameterDirection.InputOutput, true, 10, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@ERROR_MESSAGE", SqlDbType.NVarChar, 4000, ParameterDirection.InputOutput, true, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@CUSTOMER_ID", SqlDbType.Int, 4, ParameterDirection.InputOutput, true, 10, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@CUSTOMER_NAME", SqlDbType.NVarChar, 30, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@CUSTOMER_SURNAME", SqlDbType.NVarChar, 30, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@CUSTOMER_PHONENUMBER", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@CUSTOMER_ADDRESS", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                SetParameterValue(command.Parameters[0], errorNumber);
                SetParameterValue(command.Parameters[1], errorMessage);
                SetParameterValue(command.Parameters[2], customer.CustomerId);
                SetParameterValue(command.Parameters[3], customer.CustomerName);
                SetParameterValue(command.Parameters[4], customer.CustomerSurname);
                SetParameterValue(command.Parameters[5], customer.CustomerPhonenumber);
                SetParameterValue(command.Parameters[6], customer.CustomerAddress);
                ExecuteNonQuery(command);
                errorNumber = (Int32?)GetParameterValue(command.Parameters[0]);
                errorMessage = (String)GetParameterValue(command.Parameters[1]);
                HandleErrors(errorNumber, errorMessage);
            }
        }

        public void UpdateCustomer(Customer customer)
        {
            Int32? errorNumber = default(Int32?);
            String errorMessage = default(String);

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 1200;
                command.CommandText = @"[dbo].[Customers_Update]";
                command.Parameters.Add(new SqlParameter(@"@ERROR_NUMBER", SqlDbType.Int, 4, ParameterDirection.InputOutput, true, 10, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@ERROR_MESSAGE", SqlDbType.NVarChar, 4000, ParameterDirection.InputOutput, true, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@CUSTOMER_ID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@CUSTOMER_NAME", SqlDbType.NVarChar, 30, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@CUSTOMER_SURNAME", SqlDbType.NVarChar, 30, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@CUSTOMER_PHONENUMBER", SqlDbType.VarChar, 10, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@CUSTOMER_ADDRESS", SqlDbType.NVarChar, 50, ParameterDirection.Input, false, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                SetParameterValue(command.Parameters[0], errorNumber);
                SetParameterValue(command.Parameters[1], errorMessage);
                SetParameterValue(command.Parameters[2], customer.CustomerId);
                SetParameterValue(command.Parameters[3], customer.CustomerName);
                SetParameterValue(command.Parameters[4], customer.CustomerSurname);
                SetParameterValue(command.Parameters[5], customer.CustomerPhonenumber);
                SetParameterValue(command.Parameters[6], customer.CustomerAddress);
                ExecuteNonQuery(command);
                errorNumber = (Int32?)GetParameterValue(command.Parameters[0]);
                errorMessage = (String)GetParameterValue(command.Parameters[1]);
                HandleErrors(errorNumber, errorMessage);
            }
        }

        public void DeleteCustomer(Int32? customerId)
        {
            Int32? errorNumber = default(Int32?);
            String errorMessage = default(String);

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 1200;
                command.CommandText = @"[dbo].[Customers_Delete]";
                command.Parameters.Add(new SqlParameter(@"@ERROR_NUMBER", SqlDbType.Int, 4, ParameterDirection.InputOutput, true, 10, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@ERROR_MESSAGE", SqlDbType.NVarChar, 4000, ParameterDirection.InputOutput, true, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@CUSTOMER_ID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                SetParameterValue(command.Parameters[0], errorNumber);
                SetParameterValue(command.Parameters[1], errorMessage);
                SetParameterValue(command.Parameters[2], customerId);
                ExecuteNonQuery(command);
                errorNumber = (Int32?)GetParameterValue(command.Parameters[0]);
                errorMessage = (String)GetParameterValue(command.Parameters[1]);
                HandleErrors(errorNumber, errorMessage);
            }
        }

        public void DeleteCustomer(Customer customer)
        {
            Int32? errorNumber = default(Int32?);
            String errorMessage = default(String);

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 1200;
                command.CommandText = @"[dbo].[Customers_Delete]";
                command.Parameters.Add(new SqlParameter(@"@ERROR_NUMBER", SqlDbType.Int, 4, ParameterDirection.InputOutput, true, 10, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@ERROR_MESSAGE", SqlDbType.NVarChar, 4000, ParameterDirection.InputOutput, true, 0, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@CUSTOMER_ID", SqlDbType.Int, 4, ParameterDirection.Input, true, 10, 0, null, DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                SetParameterValue(command.Parameters[0], errorNumber);
                SetParameterValue(command.Parameters[1], errorMessage);
                SetParameterValue(command.Parameters[2], customer.CustomerId);
                ExecuteNonQuery(command);
                errorNumber = (Int32?)GetParameterValue(command.Parameters[0]);
                errorMessage = (String)GetParameterValue(command.Parameters[1]);
                HandleErrors(errorNumber, errorMessage);
            }
        }

        #region Handler methods

        protected virtual void SetParameterValue(SqlParameter parameter, object value, string structuredTypeName = null)
        {
            if (parameter == null)
                throw new ArgumentNullException("Sql Parameter is null.");
           
            parameter.Value = value == null ? DBNull.Value : (object)value;
        }

        private void ExecuteNonQuery(SqlCommand command)
        {
            if (command == null)
                throw new ArgumentNullException("Sql Command is null");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                command.Connection = connection;
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        protected virtual object GetParameterValue(SqlParameter parameter)
        {
            if (parameter == null)
                throw new ArgumentNullException("");

            return parameter.Value == DBNull.Value ? null : parameter.Value;
        }

        private void HandleErrors(int? errorNumber, string errorMessage)
        {
            if (errorNumber.HasValue && errorNumber.Value != 0)
            {
                errorMessage = RemoveErrorPrefix(errorMessage);
                throw new Exception(errorMessage);
            }
        }

        private static string RemoveErrorPrefix(string message)
        {
            // Deoarece mesajele de eroare din baza de date se afiseaza folosind aceasta metoda, 
            // incerc sa elimin din fata prefixul care vine din baza de date
            Regex regex = new Regex("[_a-zA-Z][_a-zA-Z0-9]+[:][0-9]+ - ", RegexOptions.Singleline);
            string[] parts = regex.Split(message);
            if (parts.Length >= 2 && string.IsNullOrEmpty(parts[0]) && !string.IsNullOrEmpty(parts[parts.Length - 1]))
                message = parts[parts.Length - 1];
            return message;
        }

        #endregion Handler methods

        #endregion Methods

        #region Properties

        private SqlConnection currentConnection;
        private SqlTransaction currentTransaction;
        private string connectionString;

        public SqlTransaction Transaction { get => currentTransaction; }
        public SqlConnection Connection { get => currentConnection; }

        #endregion Properties

    }
}
