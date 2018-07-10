using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ComicBookDataAccess
{
    static class DBConnection
    {
        internal static SqlConnection GetConnection()
        {
            //var connString = @"Data Source = LAPTOP-61UA4Q4T\SQLEXPRESS; Initial Catalog = comicOrderingDB; Integrated Security = true";
            var connString = @"Data Source=localhost;Initial Catalog=comicOrderingDB;Integrated Security=True";
            var conn = new SqlConnection(connString);
            return conn;
        }
    }
}
