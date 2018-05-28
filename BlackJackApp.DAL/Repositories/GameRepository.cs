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
    class GameRepository : IGameRepository
    {
        private BlackJackDbContext _db;

        public GameRepository()
        {
            _db = new BlackJackDbContext();
        }

        public IEnumerable<Game> GetAll()
        {
            return _db.Games;
        }

        public Game GetById(int id)
        {
            return _db.Games.Find(id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
