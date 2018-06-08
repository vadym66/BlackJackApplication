using BlackJackApp.DAL.Dapper;
using BlackJackApp.Entities.Entities;
using Dapper;
using Dapper.Contrib.Extensions;
using Dapper.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlackJackApp.DAL.Repositories
{
    public class HistoryRepository
    {
        public async Task<IEnumerable<Player>> GetGame(int gameId)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var sql = @"SELECT Players.Name
                            FROM PlayerGames
                            LEFT JOIN Players ON PlayerGames.Player_Id = Players.Id
                            WHERE PlayerGames.Game_Id = @Id";

                return await connection.QueryAsync<Player>(sql, new { Id = gameId  });
            }
        }

        public async Task<IEnumerable<Round>> GetRounds(int gameId)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var sql = @"SELECT *
                            FROM Rounds
                            JOIN Players ON Rounds.PlayerId = Players.Id
                            JOIN Cards ON Rounds.CardId = Cards.Id
                            WHERE Rounds.GameId = @Id";

                var query = await connection.QueryAsync<Round, Player, Card, Round>(sql, (round, player, card) =>
                                                                              {
                                                                                  round.Card = card;
                                                                                  round.Player = player;
                                                                                  return round;
                                                                              }, new { Id = gameId }); 
                return  query;
            }
        }
    }
}
