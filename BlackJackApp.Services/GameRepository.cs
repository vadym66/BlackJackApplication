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
    class GameRepository : IGameRepository
    {
        public IEnumerable<Game> GetAll()
        {
            throw new NotImplementedException();
        }

        public Game GetById(int id)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
