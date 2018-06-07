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
        public async Task<IEnumerable<Player>> GetGame()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return await connection.QueryAsync<Player>(@"SELECT Players.Name
                                                            FROM PlayerGames
                                                            LEFT JOIN Players ON PlayerGames.Player_Id = Players.Id
                                                            WHERE PlayerGames.Game_Id = 3");
            }
        }

        public async Task<List<Round>> GetRound(int gameId)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var sql = @"
                        SELECT *
                        FROM Rounds
                        INNER JOIN Cards ON Rounds.CardId = Cards.Id
                        WHERE Rounds.GameId = @gameid";

                var roundDictionary = new Dictionary<int, Round>();

                var query = connection.QueryAsync<Round, Card, Round>(
                                                    sql, 
                                                    (round, cards) =>
                                                    {
                                                        Round roundEntry;

                                                        if (!roundDictionary.TryGetValue(round.Id,out roundEntry))
                                                        {
                                                            roundEntry = round;
                                                            roundEntry.Cards = new List<Card>();
                                                            roundDictionary.Add(roundEntry.Id, roundEntry);
                                                        }

                                                        roundEntry.Cards.Add(cards);
                                                        return roundEntry;
                                                    }, new { gameId = gameId },
                                                    splitOn: "CardId").Result.Distinct().ToList();
                return  query;
            }
        }
    }
}
