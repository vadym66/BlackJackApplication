using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.Entities;
using BlackJackApp.Entities.Entities;

namespace BlackJackApp.DataAccess.Interface
{
    public interface IPlayerRepository
    {
        IEnumerable<Player> GetAll();
        Player GetById(int id);
        void Add(Player player);
        void Save();
        void DeleteAll();
    }
}
