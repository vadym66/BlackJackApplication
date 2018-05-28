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
    public class PlayerRepository : IPlayerRepository<Player>
    {
        private BlackJackDbContext _db;

        public PlayerRepository()
        {
            _db = new BlackJackDbContext();
        }

        public void Create(Player player)
        {
            _db.Players.Add(player);
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Player GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        IEnumerable<Player> IPlayerRepository<Player>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
