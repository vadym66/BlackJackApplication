using BlackJackApp.Entities;
using BlackJackApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.DataAccess.Interface
{
    public interface IRoundRepository<T> where T : class
    {
        Task<int> Add(Round round, int gameId);
        Task<IEnumerable<Round>> GetRounds(int gameId);
        Task<List<Round>> GetRoundsForPlayer(int gameId, string name);
    }
}
