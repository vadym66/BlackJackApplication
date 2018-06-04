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
    public class GameRepository<T> : IGameRepository<Game> where T : Game
    {
        public async Task Add(Game game)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
               await connection.ExecuteAsync("INSERT INTO Games DEFAULT VALUES");
            }
        }

        public async Task<IEnumerable<Game>> GetAll()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return await connection.QueryAsync<Game>("SELECT * FROM Games");
            }
        }

        public async Task<Game> GetLast()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return await connection.QuerySingleAsync<Game>("SELECT TOP 1 * FROM Games ORDER BY Id DESC");
            }
        }
    }
}
