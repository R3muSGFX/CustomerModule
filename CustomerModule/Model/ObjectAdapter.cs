using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace CustomerModule.Model
{
    public class ObjectAdapter
    {
        public Customer GetCustomers(int customerId)
        {
            return null;
        }

        public List<Customer> SelectCustomers()
        {
            List<Customer> customers = new List<Customer>();
            Int32? errorNumber = default(Int32?);
            String errorMessage = default(String);
            Int32? currentUserId = default(Int32?);

            using (SqlCommand command = new SqlCommand())
            {
                command.CommandType = CommandType.StoredProcedure;
                command.CommandTimeout = 1200;
                command.CommandText = @"[MR].[usp_Users_Get]";
                command.Parameters.Add(new SqlParameter(@"@ERROR_NUMBER", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.InputOutput, true, (byte)10, (byte)0, null, System.Data.DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@ERROR_MESSAGE", System.Data.SqlDbType.NVarChar, 4000, System.Data.ParameterDirection.InputOutput, true, (byte)0, (byte)0, null, System.Data.DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@CURRENT_USER_ID", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, true, (byte)10, (byte)0, null, System.Data.DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                command.Parameters.Add(new SqlParameter(@"@ID_USER", System.Data.SqlDbType.Int, 4, System.Data.ParameterDirection.Input, true, (byte)10, (byte)0, null, System.Data.DataRowVersion.Current, DBNull.Value) { UdtTypeName = null });
                SetParameterValue(command.Parameters[3], idUser);
                SetParameterValue(command.Parameters[0], errorNumber);
                SetParameterValue(command.Parameters[1], errorMessage);
                SetParameterValue(command.Parameters[2], currentUserId);

                ExecuteReader(
                    command,
                    delegate (SqlDataReader reader)
                    {
                        int[] ord = GetOrdinals(reader, @"IdUser", @"Code", @"Name", @"Account", @"PasswordHash", @"FlagDel", @"FlagServiceProvider", @"RowDate", @"RowUserId", @"Email", @"Phone", @"IdOrganizationUnit", @"FlagBusinessRole", @"OrganizationUnitCode", @"OrganizationUnitName");
                        if (reader.Read())
                        {
                            current.IdUser = Copy(current.IdUser, reader, ord[0]);
                            current.Code = Copy(current.Code, reader, ord[1]);
                            current.Name = Copy(current.Name, reader, ord[2]);
                            current.Account = Copy(current.Account, reader, ord[3]);
                            current.PasswordHash = Copy(current.PasswordHash, reader, ord[4]);
                            current.FlagDel = Copy(current.FlagDel, reader, ord[5]);
                            current.FlagServiceProvider = Copy(current.FlagServiceProvider, reader, ord[6]);
                            current.RowDate = Copy(current.RowDate, reader, ord[7]);
                            current.RowUserId = Copy(current.RowUserId, reader, ord[8]);
                            current.Email = Copy(current.Email, reader, ord[9]);
                            current.Phone = Copy(current.Phone, reader, ord[10]);
                            current.IdOrganizationUnit = Copy(current.IdOrganizationUnit, reader, ord[11]);
                            current.FlagBusinessRole = Copy(current.FlagBusinessRole, reader, ord[12]);
                            current.OrganizationUnitCode = Copy(current.OrganizationUnitCode, reader, ord[13]);
                            current.OrganizationUnitName = Copy(current.OrganizationUnitName, reader, ord[14]);
                            result.Add(current);
                        }

                        while (reader.Read()) ;
                    });

                errorNumber = (Int32?)GetParameterValue(command.Parameters[0]);
                errorMessage = (String)GetParameterValue(command.Parameters[1]);
                HandleErrors(errorNumber, errorMessage);
            }

            return customers;
        }

        protected virtual void SetParameterValue(SqlParameter parameter, object value, string structuredTypeName = null)
        {
            if (parameter == null)
                throw new ArgumentNullException("");
           
            parameter.Value = value == null ? DBNull.Value : (object)value;
        }

        public void ExecuteReader(SqlCommand command, Action<SqlDataReader> action)
        {
            if (command == null)
                throw new ArgumentNullException("");

            if (action == null)
                throw new ArgumentNullException("");

            if (this.currentConnection == null)
            {
                // Creez o conexiune noua
                using (SqlConnection connection = new SqlConnection(this.ConnectionString))
                {
                    connection.Open();
                    command.Connection = connection;
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        action(reader);
                    }
                }
            }
            else
            {
                // Folosesc conexiunea si tranzactia existente
                command.Connection = this.currentConnection;
                command.Transaction = this.currentTransaction;
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    action(reader);
                }
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

        public static string RemoveErrorPrefix(string message)
        {
            // Deoarece mesajele de eroare din baza de date se afiseaza folosind aceasta metoda, 
            // incerc sa elimin din fata prefixul care vine din baza de date
            Regex regex = new Regex("[_a-zA-Z][_a-zA-Z0-9]+[:][0-9]+ - ", RegexOptions.Singleline);
            string[] parts = regex.Split(message);
            if (parts.Length >= 2 && string.IsNullOrEmpty(parts[0]) && !string.IsNullOrEmpty(parts[parts.Length - 1]))
                message = parts[parts.Length - 1];
            return message;
        }

        protected virtual T Convert<T>(object value)
        {
            if (value == DBNull.Value)
                return default(T);
            return (T)value;
        }
        
        protected virtual T Copy<T>(T defaultValue, IDataReader reader, int index)
        {
            if (reader == null)
                throw new ArgumentNullException("");

            if (index < 0 || reader.IsDBNull(index))
                return defaultValue;

            object value = reader.GetValue(index);
            return Convert<T>(value);
        }

        protected virtual int[] GetOrdinals(IDataReader reader, params string[] names)
        {
            if (reader == null)
                throw new ArgumentNullException(Properties.Resources.AdapterBase04);

            if (names == null)
                throw new ArgumentNullException(Properties.Resources.AdapterBase05);

            // Extrag coloanele din reader
            int count = reader.FieldCount;
            string[] columnNames = new string[count];
            for (int i = 0; i < count; i++)
                columnNames[i] = Naming.Convert(reader.GetName(i), NamingStyle.Pascal);

            // Extrag ordinalele
            int[] ord = new int[names.Length];
            for (int i = 0; i < ord.Length; i++)
            {
                ord[i] = -1;
                for (int j = 0; j < count; j++)
                {
                    int k = (i + j) % count;
                    if (string.Equals(columnNames[k], names[i]))
                    {
                        ord[i] = k;
                        break;
                    }
                }
            }

            return ord;
        }

        private SqlConnection currentConnection = null;
        private SqlTransaction currentTransaction = null;
        private string ConnectionString = "";
    }
}
