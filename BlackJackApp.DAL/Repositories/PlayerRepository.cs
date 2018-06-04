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
    public class PlayerRepository<T> : IPlayerRepository<Player> where T : Player
    {
        //insert Player to database
        public async Task Add(Player player)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                await connection.ExecuteAsync("INSERT INTO Players(Name) VALUES(@Name)", new { player.Name });
            }
        }

        public async Task<IEnumerable<Player>> GetAll()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return await connection.QueryAsync<Player>("SELECT * FROM Players");
            }
        }

        public async Task<Player> GetLast()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return await connection.QuerySingleAsync<Player>("SELECT TOP 1 * FROM Players ORDER BY Id DESC");
            }
        }
    }
}
