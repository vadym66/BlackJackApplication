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
    public class RoundRepository : IRoundRepository
    {
        public void Add(Round round)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                connection.Execute("INSERT INTO Round(PlayerId, CardId, Game_id) VALUES(@Name)");
            }
        }

        public void Add(Round round, int game_id)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                connection.Execute("INSERT INTO Round(PlayerId, CardId, Game_id) VALUES(@Name)");
            }
        }

        public IEnumerable<Round> GetAll()
        {
            throw new NotImplementedException();
        }



        public Round GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            
        }
    }
}
