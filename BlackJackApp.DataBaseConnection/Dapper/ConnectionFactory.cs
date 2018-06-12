using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;

namespace BlackJackApp.DAL.Dapper
{
    public class ConnectionFactory
    {
        public static DbConnection GetOpenDbConnection()
        {
            var connection = new SqlConnection(WebConfigurationManager.ConnectionStrings["BlackJackDb"].ConnectionString);
            connection.Open();

            return connection;
        }
    }
}
