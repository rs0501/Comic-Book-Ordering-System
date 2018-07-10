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
    public class UserAccessor
    {
        public static int VerifyLoginInfo(string email)
        {
            var result = 0;
            var conn = DBConnection.GetConnection();

            var cmdText = @"sp_authenticate_user";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Email", SqlDbType.VarChar, 50);
            //cmd.Parameters.Add("@PasswordHash", SqlDbType.VarChar, 100);
            cmd.Parameters["@Email"].Value = email;
            //cmd.Parameters["@PasswordHash"].Value = passwordHash;

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

        public static User RetrieveUserWithEmail(string email)
        {
            User _user = null;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_user_by_email_login";
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
                    _user = new User()
                    {
                        UserID = reader.GetInt32(0),
                        Email = reader.GetString(1),
                        //PasswordHash = reader.GetString(2),
                        Active = reader.GetBoolean(2)
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

            return _user;
        }

        public static List<Role> RetrieveUserRoles(int userId)
        {
            var roles = new List<Role>();
            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_user_roles";

            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@UserID", SqlDbType.Int);
            cmd.Parameters["@UserID"].Value = userId;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        roles.Add(new Role()
                        {
                            UserID = reader.GetInt32(0),
                            RoleID = reader.GetString(1)
                        });
                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                conn.Close();
            }

            return roles;
        }
    }
}
