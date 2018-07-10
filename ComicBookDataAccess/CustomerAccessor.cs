using ComicBookDataObjects;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicBookDataAccess
{
    public class CustomerAccessor
    {
        public static int VerifyLoginInfo(string email, string passwordHash)
        {
            var result = 0;
            var conn = DBConnection.GetConnection();
            
            var cmdText = @"sp_authenticate_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar, 100);
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = passwordHash;

            try
            {
                conn.Open();
                result = (int)cmd.ExecuteScalar();
            }
            catch (Exception)
            {
                throw new Exception("An error occurred logging you in.");
            }
            finally
            {
                conn.Close();
            }


            return result;
        }

        public static int UpdateCustomerEmail(int? customerID, string newEmail)
        {
            var count = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_customer_email";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);

            cmd.Parameters["@CustomerID"].Value = customerID;
            cmd.Parameters["@Email"].Value = newEmail;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw new Exception("An error occurred changing email.");
            }
            finally
            {
                conn.Close();
            }

            return count;
        }

        public static int UpdateCustomerPasswordHash(int? customerID, string oldPasswordHash, string newPasswordHash)
        {
            var count = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_customer_passwordHash";
            var cmd = new SqlCommand(cmdText, conn);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
            cmd.Parameters.Add("@OldPasswordHash", SqlDbType.VarChar, 100);
            cmd.Parameters.Add("@NewPasswordHash", SqlDbType.VarChar, 100);

            cmd.Parameters["@CustomerID"].Value = customerID;
            cmd.Parameters["@OldPasswordHash"].Value = oldPasswordHash;
            cmd.Parameters["@NewPasswordHash"].Value = newPasswordHash;

            try
            {
                conn.Open();
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conn.Close();
            }
            return count;
        }

        public static Customer RetrieveCustomerWithEmail(string email)
        {
            Customer _customer = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_customer_by_email_login";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if(reader.HasRows)
                {
                    reader.Read();
                    _customer = new Customer()
                    {
                        CustomerID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Zip = reader.GetString(4),
                        Email = reader.GetString(5),
                        Active = reader.GetBoolean(6)
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("A problem occurred with retrieving customer info." + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return _customer;
        }

        public static List<Customer> RetrieveCurrentCustomers(bool active)
        {
            var customers = new List<Customer>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_customer_by_active";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = active;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        customers.Add(new Customer()
                        {
                            CustomerID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Zip = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            Email = reader.GetString(5),
                            Active = reader.GetBoolean(6)
                        });
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.StackTrace + "A problem occurred acccessing database for customers.");
            }
            finally
            {
                conn.Close();
            }

            return customers;
        }

        public static List<Customer> RetrieveDeactivatedCustomers(bool active)
        {
            var customers = new List<Customer>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_customer_by_inactive";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Active", SqlDbType.Bit);
            cmd.Parameters["@Active"].Value = active;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        customers.Add(new Customer()
                        {
                            CustomerID = reader.GetInt32(0),
                            FirstName = reader.GetString(1),
                            LastName = reader.GetString(2),
                            Zip = reader.GetString(3),
                            PhoneNumber = reader.GetString(4),
                            Email = reader.GetString(5),
                            Active = reader.GetBoolean(6)
                        });
                    }
                    reader.Close();
                }

            }
            catch (Exception ex)
            {

                throw new Exception(ex.StackTrace + "A problem occurred acccessing database for customers.");
            }
            finally
            {
                conn.Close();
            }

            return customers;
        }

        public static int AddNewCustomer(string firstName, string lastName, string phoneNum, 
                                                    string zip, string email)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_new_customer";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@FirstName", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@LastName", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@PhoneNumber", SqlDbType.VarChar, 10);
            cmd.Parameters.Add("@Zip", SqlDbType.VarChar, 5);
            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
            cmd.Parameters["@FirstName"].Value = firstName;
            cmd.Parameters["@LastName"].Value = lastName;
            cmd.Parameters["@PhoneNumber"].Value = phoneNum;
            cmd.Parameters["@Zip"].Value = zip;
            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("A problem occurred creating your account." + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public static int UpdateCustomerInfo(int? customerID, string oldFirstName, string oldLastName, string oldPhoneNum, string oldZip, string oldEmail, bool oldActive,
                             string firstName, string lastName, string phoneNum, string zip, string email, bool active)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_customer_record";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
            cmd.Parameters.Add("@OldFirstName", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@OldLastName", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@OldPhoneNumber", SqlDbType.VarChar, 10);
            cmd.Parameters.Add("@OldZip", SqlDbType.VarChar, 5);
            cmd.Parameters.Add("@OldEmail", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@OldActive", SqlDbType.Bit);

            cmd.Parameters.Add("@NewFirstName", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@NewLastName", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@NewPhoneNumber", SqlDbType.VarChar, 10);
            cmd.Parameters.Add("@NewZip", SqlDbType.VarChar, 5);
            cmd.Parameters.Add("@NewEmail", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@NewActive", SqlDbType.Bit);

            cmd.Parameters["@CustomerID"].Value = customerID;
            cmd.Parameters["@OldFirstName"].Value = oldFirstName;
            cmd.Parameters["@OldLastName"].Value = oldLastName;
            cmd.Parameters["@OldPhoneNumber"].Value = oldPhoneNum;
            cmd.Parameters["@OldZip"].Value = oldZip;
            cmd.Parameters["@OldEmail"].Value = oldEmail;
            cmd.Parameters["@OldActive"].Value = oldActive;

            cmd.Parameters["@NewFirstName"].Value = firstName;
            cmd.Parameters["@NewLastName"].Value = lastName;
            cmd.Parameters["@NewPhoneNumber"].Value = phoneNum;
            cmd.Parameters["@NewZip"].Value = zip;
            cmd.Parameters["@NewEmail"].Value = email;
            cmd.Parameters["@NewActive"].Value = active;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

                throw new ApplicationException("A problem occurred updating your account." + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public static List<Comic> RetrieveCustomerPull(int? customerID)
        {
            var pullList = new List<Comic>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_comic_by_customer_pull";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
            cmd.Parameters["@CustomerID"].Value = customerID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        pullList.Add(new Comic()
                        {
                            ComicID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Issue = reader.GetInt32(2),
                            Authors = reader.GetString(3),
                            Publisher = reader.GetString(4),
                            Rating = reader.GetString(5),
                            Description = reader.GetString(6),
                            Price = reader.GetDecimal(7)
                        });
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return pullList;
        }

        public static List<Comic> RetrieveCustomerOrder(int? customerID)
        {
            var orderForm = new List<Comic>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_comics_by_customer_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
            cmd.Parameters["@CustomerID"].Value = customerID;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        orderForm.Add(new Comic()
                        {
                            ComicID = reader.GetInt32(0),
                            Title = reader.GetString(1),
                            Issue = reader.GetInt32(2),
                            Authors = reader.GetString(3),
                            Publisher = reader.GetString(4),
                            Rating = reader.GetString(5),
                            Description = reader.GetString(6),
                            Price = reader.GetDecimal(7)
                        });
                    }
                    reader.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return orderForm;
        }

        public static Customer RetrieveCustomerWithID(int? id)
        {
            Customer _customer = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_customer_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@CustomerID", SqlDbType.VarChar, 50);
            cmd.Parameters["@CustomerID"].Value = id;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    _customer = new Customer()
                    {
                        CustomerID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        PhoneNumber = reader.GetString(3),
                        Zip = reader.GetString(4),
                        Email = reader.GetString(5),
                        Active = reader.GetBoolean(6)
                    };
                }
                reader.Close();
            }
            catch (Exception ex)
            {

                throw new Exception("A problem occurred with retrieving customer info." + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return _customer;
        }
    }
}
