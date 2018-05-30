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
    public class PlayerRepository : IPlayerRepository
    {
        //insert Player to database
        public void Add(Player player)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                connection.Execute("INSERT INTO Players(Name) VALUES(@Name)", new { player.Name });
            }
        }

        public void DeleteAll()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                connection.Execute("DELETE FROM Players");
            }
        }

        public IEnumerable<Player> GetAll()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return connection.Query<Player>("SELECT * FROM Players").ToList();
            }
        }

        public Player GetById(int id)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return connection.Query<Player>("SELECT * FROM Players WHERE Id=@id", new { id }).First();
            }
        }

        public Player GetLast()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return connection.Query<Player>("SELECT TOP 1 * FROM Players ORDER BY Id DESC").First();
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }

}
