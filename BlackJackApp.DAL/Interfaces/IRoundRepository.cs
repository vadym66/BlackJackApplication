using BlackJackApp.Entities;
using BlackJackApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.DataAccess.Interface
{
    public interface IRoundRepository
    {
        void Add(Round round, int game_id);
        IEnumerable<Round> GetAll();
        Round GetById(int id);
        void Save();
    }
}
