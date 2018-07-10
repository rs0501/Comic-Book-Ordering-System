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
    public class EmployeeAccessor
    {
        public static int VerifyEmpLoginInfo(string email, string password)
        {
            var result = 0;
            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_authenticate_userEmployee";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
            cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar, 100);
            cmd.Parameters["@Email"].Value = email;
            cmd.Parameters["@PasswordHash"].Value = password;

            try
            {
                conn.Open();
                result = (int)cmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                throw new Exception(ex.Message + ex.StackTrace);
            }
            finally
            {
                conn.Close();
            }


            return result;
        }
        public static Employee RetrieveEmployeeWithEmail(string email)
        {
            Employee _employee = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_employee_by_email";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
            cmd.Parameters["@Email"].Value = email;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    _employee = new Employee()
                    {
                        EmployeeID = reader.GetInt32(0),
                        FirstName = reader.GetString(1),
                        LastName = reader.GetString(2),
                        Email = reader.GetString(3)
                    };
                }
                reader.Close();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                conn.Close();
            }

            return _employee;
        }

        public static int UpdateEmployee(int? employeeID, string oldFirstName, string oldLastName, string oldPhoneNum, string oldZip, string oldEmail, bool oldActive,
                             string firstName, string lastName, string phoneNum, string zip, string email, bool active)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_employee_record";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EmployeeID", SqlDbType.Int);
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

            cmd.Parameters["@EmployeeID"].Value = employeeID;
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

        public static int CreateEmployee(string firstName, string lastName, string phoneNum,
                                                    string zip, string email)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_create_new_employee";
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

        public static Employee RetrieveEmployeeWithID(int? id)
        {
            Employee _employee = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_employee_by_id";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@EmployeeID", SqlDbType.VarChar, 50);
            cmd.Parameters["@EmployeeID"].Value = id;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read();
                    _employee = new Employee()
                    {
                        EmployeeID = reader.GetInt32(0),
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

                throw new Exception("A problem occurred with retrieving employee info." + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return _employee;
        }
    }
}
