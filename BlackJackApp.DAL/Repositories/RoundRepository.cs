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

namespace BlackJackApp.Services
{
    public class RoundRepository<T> : IRoundRepository<Round> where T : Round
    {
        public async Task Add(Round round, int gameId)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                await connection.ExecuteAsync("INSERT INTO Rounds(Player_Id, Card_Id, Game_Id) VALUES(@PlayerId, @CardId, @gameId)", new {round.PlayerId, round.CardId, gameId });
            }
        }
    }
}
