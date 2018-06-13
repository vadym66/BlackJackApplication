using BlackJackApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.DAL.Interfaces
{
    public interface IPlayersGameRepository
    {
        Task UpdatePlayerStatus(Player player);
    }
}
