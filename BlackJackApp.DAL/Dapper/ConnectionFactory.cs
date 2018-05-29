using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlackJackApp.DAL.Dapper
{
    public class ConnectionFactory
    {
        public static DbConnection GetOpenDbConnection()
        {
            var connection = new SqlConnection("Data Source=DESKTOP-3M9Q6SI;Initial Catalog=BlackJackDb-ByConnectionString;Integrated Security=True");
            connection.Open();

            return connection;
        }
    }
}
