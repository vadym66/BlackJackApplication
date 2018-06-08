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
using Dapper.Contrib.Extensions;

namespace BlackJackApp.Services
{
    public class RoundRepository<T> : IRoundRepository<Round> where T : Round
    {
        public async Task<int> Add(Round round, int gameId)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                var id = await connection.QueryAsync<int>("INSERT INTO Rounds(PlayerId, CardId, GameId) OUTPUT Inserted.ID VALUES(@PlayerId, @CardId, @gameId)",
                                                            new {round.PlayerId, round.CardId, gameId });
                return id.Single();
            }
        }
    }
}
