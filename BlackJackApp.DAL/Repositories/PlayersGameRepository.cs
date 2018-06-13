using BlackJackApp.DAL.Interfaces;
using BlackJackApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.DAL.Repositories
{
    public class PlayersGameRepository : IPlayersGameRepository
    {
        public Task UpdatePlayerStatus(Player player)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var sql = @"INSERT INTO Games 
                            OUTPUT Inserted.ID DEFAULT VALUES";

                return (await connection.QueryAsync<int>(sql, new { Game = game })).Single();
            }
        }
    }
}
