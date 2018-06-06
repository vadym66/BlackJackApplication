using BlackJackApp.DAL.Dapper;
using BlackJackApp.Entities.Entities;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.DAL.Repositories
{
    public class HistoryRepository
    {
        public async Task<Game> GetLastGame()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return await connection.QuerySingleAsync<Game>("SELECT TOP 1 * FROM Games ORDER BY Id DESC");
            }
        }
    }
}
