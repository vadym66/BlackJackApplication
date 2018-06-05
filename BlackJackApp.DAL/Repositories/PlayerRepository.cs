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
        public async Task<int> Add(Player player)
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
                return await connection.QueryAsync<Player>("SELECT * FROM Players");
            }
        }
    }
}
