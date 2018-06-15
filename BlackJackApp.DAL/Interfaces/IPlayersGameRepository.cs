using BlackJackApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.DAL.Interfaces
{
    public interface IPlayersGameRepository<T> where T : class
    {
        Task AddPlayerStatus(Player player, string status);
        Task AddPlayer(Player player, int gameId);
    }
}
