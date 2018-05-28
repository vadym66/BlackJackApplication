using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.DAL.EF;
using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities;
using BlackJackApp.Entities.Entities;

namespace BlackJackApp.Services
{
    public class RoundRepository : IRoundRepository
    {
        private BlackJackDbContext _db;

        public RoundRepository()
        {
            _db = new BlackJackDbContext();
        }

        public void Create(Round round)
        {
            _db.Rounds.Add(round);
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
            _db.SaveChanges();
        }
    }
}
