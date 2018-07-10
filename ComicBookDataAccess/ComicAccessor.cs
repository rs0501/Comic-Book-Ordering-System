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
    public class ComicAccessor
    {
        public static List<Comic> RetrieveComicByStatus(bool status)
        {
            var comics = new List<Comic>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_comic_by_status";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Status", SqlDbType.Bit);
            cmd.Parameters["@Status"].Value = status;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comics.Add(new Comic()
                        {
                            ComicID = reader.GetInt32(0),
                            ComicType = reader.GetString(1),
                            DistributorID = reader.GetInt32(2),
                            Title = reader.GetString(3),
                            Issue = reader.GetInt32(4),
                            Authors = reader.GetString(5),
                            Publisher = reader.GetString(6),
                            Rating = reader.GetString(7),
                            Description = reader.GetString(8),
                            Quantity = reader.GetInt32(9),
                            Price = reader.GetDecimal(10),
                            Status = reader.GetBoolean(11)
                        });
                    }
                    reader.Close();
                }
            }
            catch (Exception)
            {

                throw new Exception("An error occurred accessing comics");
            }
            finally
            {
                conn.Close();
            }

            return comics;
        }


        public static List<Comic> RetrieveComicByTitle(string title)
        {
            var comics = new List<Comic>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_comic_by_title";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Title", SqlDbType.VarChar, 250);
            cmd.Parameters["@Title"].Value = title;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        comics.Add(new Comic()
                        {
                            ComicID = reader.GetInt32(0),
                            ComicType = reader.GetString(1),
                            DistributorID = reader.GetInt32(2),
                            Title = reader.GetString(3),
                            Issue = reader.GetInt32(4),
                            Authors = reader.GetString(5),
                            Publisher = reader.GetString(6),
                            Rating = reader.GetString(7),
                            Description = reader.GetString(8),
                            Quantity = reader.GetInt32(9),
                            Price = reader.GetDecimal(10),
                            Status = reader.GetBoolean(11)
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

            return comics;
        }

        public static List<Comic> RetrieveComicByAuthors(string authors)
        {
            var comic = new List<Comic>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_comic_by_authors";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Authors", SqlDbType.VarChar, 250);
            cmd.Parameters["@Authors"].Value = authors;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comic.Add(new Comic()
                        {
                            ComicID = reader.GetInt32(0),
                            ComicType = reader.GetString(1),
                            DistributorID = reader.GetInt32(2),
                            Title = reader.GetString(3),
                            Issue = reader.GetInt32(4),
                            Authors = reader.GetString(5),
                            Publisher = reader.GetString(6),
                            Rating = reader.GetString(7),
                            Description = reader.GetString(8),
                            Quantity = reader.GetInt32(9),
                            Price = reader.GetDecimal(10),
                            Status = reader.GetBoolean(11)
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

            return comic;
        }

        public static List<Comic> RetrieveComicByPublisher(string publisher)
        {
            var comic = new List<Comic>();

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_retrieve_comic_by_publisher";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Publisher", SqlDbType.VarChar, 200);
            cmd.Parameters["@Publisher"].Value = publisher;

            try
            {
                conn.Open();
                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        comic.Add(new Comic()
                        {
                            ComicID = reader.GetInt32(0),
                            ComicType = reader.GetString(1),
                            DistributorID = reader.GetInt32(2),
                            Title = reader.GetString(3),
                            Issue = reader.GetInt32(4),
                            Authors = reader.GetString(5),
                            Publisher = reader.GetString(6),
                            Rating = reader.GetString(7),
                            Description = reader.GetString(8),
                            Quantity = reader.GetInt32(9),
                            Price = reader.GetDecimal(10),
                            Status = reader.GetBoolean(11)
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
            return comic;
        }

        public static int AddComic(string title, int issue, string authors, string publisher, string comicType, 
                                        int distributorID, string rating, string description, int quantity, decimal price, bool status)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_insert_comic";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Title", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@Issue", SqlDbType.Int);
            cmd.Parameters.Add("@Authors", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@Publisher", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@ComicType", SqlDbType.VarChar, 25);
            cmd.Parameters.Add("@DistributorID", SqlDbType.Int);
            cmd.Parameters.Add("@Rating", SqlDbType.VarChar, 25);
            cmd.Parameters.Add("@Description", SqlDbType.VarChar, 750);
            cmd.Parameters.Add("@Quantity", SqlDbType.Int);
            cmd.Parameters.Add("@Price", SqlDbType.Decimal);
            cmd.Parameters.Add("@Status", SqlDbType.Bit);

            cmd.Parameters["@Title"].Value = title;
            cmd.Parameters["@Issue"].Value = issue;
            cmd.Parameters["@Authors"].Value = authors;
            cmd.Parameters["@Publisher"].Value = publisher;
            cmd.Parameters["@ComicType"].Value = comicType;
            cmd.Parameters["@DistributorID"].Value = distributorID;
            cmd.Parameters["@Rating"].Value = rating;
            cmd.Parameters["@Description"].Value = description;
            cmd.Parameters["@Quantity"].Value = quantity;
            cmd.Parameters["@Price"].Value = price;
            cmd.Parameters["@Status"].Value = status;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw new ApplicationException("A problem occurred a comic adding to database.");
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public static int UpdateComic(int? comicID, string oldTitle, int oldIssue, string oldAuthors, string oldPublisher, string oldComicType,
                                        int oldDistributorID, string oldRating, string oldDescription, int oldQuantity, decimal oldPrice, bool oldStatus,
                                        string title, int issue, string authors, string publisher, string comicType,
                                        int distributorID, string rating, string description, int quantity, decimal price, bool status)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_update_comic_record";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@ComicID", SqlDbType.Int);
            cmd.Parameters.Add("@OldTitle", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@OldIssue", SqlDbType.Int);
            cmd.Parameters.Add("@OldAuthors", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@OldPublisher", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@OldComicType", SqlDbType.VarChar, 25);
            cmd.Parameters.Add("@OldDistributorID", SqlDbType.Int);
            cmd.Parameters.Add("@OldRating", SqlDbType.VarChar, 25);
            cmd.Parameters.Add("@OldDescription", SqlDbType.VarChar, 750);
            cmd.Parameters.Add("@OldQuantity", SqlDbType.Int);
            cmd.Parameters.Add("@OldPrice", SqlDbType.Decimal);
            cmd.Parameters.Add("@OldStatus", SqlDbType.Bit);

            cmd.Parameters.Add("@NewTitle", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@NewIssue", SqlDbType.Int);
            cmd.Parameters.Add("@NewAuthors", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@NewPublisher", SqlDbType.VarChar, 250);
            cmd.Parameters.Add("@NewComicType", SqlDbType.VarChar, 25);
            cmd.Parameters.Add("@NewDistributorID", SqlDbType.Int);
            cmd.Parameters.Add("@NewRating", SqlDbType.VarChar, 25);
            cmd.Parameters.Add("@NewDescription", SqlDbType.VarChar, 750);
            cmd.Parameters.Add("@NewQuantity", SqlDbType.Int);
            cmd.Parameters.Add("@NewPrice", SqlDbType.Decimal);
            cmd.Parameters.Add("@NewStatus", SqlDbType.Bit);

            cmd.Parameters["@ComicID"].Value = comicID;
            cmd.Parameters["@OldTitle"].Value = oldTitle;
            cmd.Parameters["@OldIssue"].Value = oldIssue;
            cmd.Parameters["@OldAuthors"].Value = oldAuthors;
            cmd.Parameters["@OldPublisher"].Value = oldPublisher;
            cmd.Parameters["@OldComicType"].Value = oldComicType;
            cmd.Parameters["@OldDistributorID"].Value = oldDistributorID;
            cmd.Parameters["@OldRating"].Value = oldRating;
            cmd.Parameters["@OldDescription"].Value = oldDescription;
            cmd.Parameters["@OldQuantity"].Value = oldQuantity;
            cmd.Parameters["@OldPrice"].Value = oldPrice;
            cmd.Parameters["@OldStatus"].Value = oldStatus;

            cmd.Parameters["@NewTitle"].Value = title;
            cmd.Parameters["@NewIssue"].Value = issue;
            cmd.Parameters["@NewAuthors"].Value = authors;
            cmd.Parameters["@NewPublisher"].Value = publisher;
            cmd.Parameters["@NewComicType"].Value = comicType;
            cmd.Parameters["@NewDistributorID"].Value = distributorID;
            cmd.Parameters["@NewRating"].Value = rating;
            cmd.Parameters["@NewDescription"].Value = description;
            cmd.Parameters["@NewQuantity"].Value = quantity;
            cmd.Parameters["@NewPrice"].Value = price;
            cmd.Parameters["@NewStatus"].Value = status;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw new ApplicationException("A problem occurred a comic adding to database.");
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public static int AddToPullList(int? customerID, int? comicID)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_add_comic_to_pullist";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
            cmd.Parameters.Add("@ComicID", SqlDbType.Int);

            cmd.Parameters["@CustomerID"].Value = customerID;
            cmd.Parameters["@ComicID"].Value = comicID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw new ApplicationException("A problem occurred adding comic to pull list.");
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public static int RemoveFromPullList(int? comicID, int? customerID)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_remove_comic_from_pull";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ComicID", SqlDbType.Int);
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);

            cmd.Parameters["@ComicID"].Value = comicID;
            cmd.Parameters["@CustomerID"].Value = customerID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw new ApplicationException("A problem occurred removing comic to pull list.");
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        public static int AddToOrder(int? customerID, int? comicID, DateTime date)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_add_comic_to_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);
            cmd.Parameters.Add("@ComicID", SqlDbType.Int);
            cmd.Parameters.Add("@Date", SqlDbType.DateTime);

            cmd.Parameters["@CustomerID"].Value = customerID;
            cmd.Parameters["@ComicID"].Value = comicID;
            cmd.Parameters["@Date"].Value = date;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw new ApplicationException("A problem occurred a comic adding to your order.");
            }
            finally
            {
                conn.Close();
            }

            return result;
        }
        public static int RemoveFromOrderForm(int? comicID, int? customerID)
        {
            int result = 0;

            var conn = DBConnection.GetConnection();
            var cmdText = @"sp_remove_comic_from_order";
            var cmd = new SqlCommand(cmdText, conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@ComicID", SqlDbType.Int);
            cmd.Parameters.Add("@CustomerID", SqlDbType.Int);

            cmd.Parameters["@ComicID"].Value = comicID;
            cmd.Parameters["@CustomerID"].Value = customerID;

            try
            {
                conn.Open();
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw new ApplicationException("A problem occurred removing comic to pull list.");
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        //public static decimal RetrieveOrderTotal(int? customerID, int comicID)
        //{
        //    decimal result = 0;
        //    var conn = DBConnection.GetConnection();

        //    var cmdText = @"sp_calculate_order_total";
        //    var cmd = new SqlCommand(cmdText, conn);
        //    cmd.CommandType = CommandType.StoredProcedure;

        //    cmd.Parameters.Add("@CustomerID", SqlDbType.VarChar, 50);
        //    cmd.Parameters.Add("@ComicID", SqlDbType.VarChar, 100);
        //    cmd.Parameters["@CustomerID"].Value = customerID;
        //    cmd.Parameters["@ComicID"].Value = comicID;

        //    try
        //    {
        //        conn.Open();
        //        result = (decimal)cmd.ExecuteScalar();
        //    }
        //    catch (Exception)
        //    {
        //        throw new ApplicationException("An error occurred getting the total.");
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }

        //    return result;
        //}
    }
}
