using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.DAL.Dapper;
using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Entities.Enums;
using BlackJackApp.Services;
using Dapper;

namespace BlackJackApp.Services
{
    public class CardRepository : ICardRepository
    {

        public Card GetRandom()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return connection.Query<Card>("SELECT TOP 1 * FROM Cards ORDER BY newid()").First();
            }
        }

        public IEnumerable<Card> GetAll()
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return connection.Query<Card>("SELECT * FROM Cards").ToList();
            }
        }

        public Card GetById(int id)
        {
            using (var connection = ConnectionFactory.GetOpenDbConnection())
            {
                return connection.Query<Card>("SELECT * FROM Cards WHERE Id=@id", new { id }).First();
            }
        }
    }
}
