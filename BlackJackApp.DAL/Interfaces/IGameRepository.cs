using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.Entities.Entities;

namespace BlackJackApp.DataAccess.Interface
{
    public interface IGameRepository
    {
        IEnumerable<Game> GetAll();
        Game GetById(int id);
        void Save();
    }
}
