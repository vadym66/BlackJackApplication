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
    public class GameRepository : IGameRepository
    {
        public void Add(Game game)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                connection.Execute("INSERT INTO Games DEFAULT VALUES");
            }
        }

        public void DeleteAll()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                connection.Execute("DELETE FROM Games");
            }
        }

        public IEnumerable<Game> GetAll()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return connection.Query<Game>("SELECT * FROM Games").ToList();
            }
        }

        public Game GetById(int id)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return connection.Query<Game>("SELECT * FROM Cards WHERE Id=@id", new { id }).First();
            }
        }

        public Game GetLast()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return connection.Query<Game>("SELECT TOP 1 * FROM Games ORDER BY Id DESC").First();
            }
        }

        public void Save()
        {
            
        }
    }
}
