using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.Entities;
using BlackJackApp.Entities.Entities;

namespace BlackJackApp.DataAccess.Interface
{
    public interface IPlayerRepository<T> where T : class
    {
        Task<int> Add(Player player, int gameId);
        Task<IEnumerable<Player>> GetAll();
        Task<IEnumerable<Player>> GetBots(int botNumber);
        Task<Player> GetDealer();
    }
}
