using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.DAL.Dapper;
using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities;
using BlackJackApp.Entities.Entities;
using Dapper;
using Dapper.Contrib;
using Dapper.Contrib.Extensions;

namespace BlackJackApp.Services
{
    public class PlayerRepository<T> : IPlayerRepository<Player> where T : Player
    {
        //insert Player to database
        public async Task<int> Add(Player player, int gameId)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return await connection.InsertAsync(player);
            }
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var sql = @"SELECT * 
                            FROM Players";

                return await connection.QueryAsync<Player>(sql);
            }
        }

        public async Task<IEnumerable<Player>> GetBots(int botNumber)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var sql = @"SELECT TOP (@BotNumber) * 
                            FROM Players WHERE Players.Id  BETWEEN 2 AND 5";

                return await connection.QueryAsync<Player>(sql, new { BotNumber = botNumber });
            }
        }

        public async Task<Player> GetDealer()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var sql = @"SELECT *
                            FROM Players
                            WHERE Players.Id = 1";

                return await connection.QuerySingleAsync<Player>(sql);
            }
        }
    }
}
