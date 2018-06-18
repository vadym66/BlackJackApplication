using BlackJackApp.DAL.Dapper;
using BlackJackApp.DAL.Interfaces;
using BlackJackApp.Entities.Entities;
using Dapper;
using Dapper.Contrib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.DAL.Repositories
{
    public class PlayersGameRepository<T> : IPlayersGameRepository<Player>
    {
        public async Task AddPlayer(Player player, int gameId)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var sql = @"INSERT INTO PlayerGames(PlayerId, GameId)
                            VALUES(@PlayerId, @gameId)";

                await connection.ExecuteAsync(sql, new { PlayerId = player.Id, GameId = gameId });
            }
        }

        public async Task AddPlayerStatus(Player player, string status)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var sql = @"INSERT INTO PlayerGames(PlayerStatus) 
                            VALUES(@PlayerStatus)";

                await connection.QueryAsync<int>(sql, new { PlayerStatus = status });
            }
        }
    }
}
