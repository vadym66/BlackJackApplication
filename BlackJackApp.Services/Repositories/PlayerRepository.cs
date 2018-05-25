using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities;
using BlackJackApp.Entities.Entities;

namespace BlackJackApp.Services
{
    class PlayerRepository : IPlayerRepository<Player>
    {
        public void Create()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        IEnumerable<Player> IPlayerRepository<Player>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
