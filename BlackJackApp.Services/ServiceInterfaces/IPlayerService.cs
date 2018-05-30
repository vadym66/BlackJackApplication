using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services.ServiceInterfaces
{
    public interface IPlayerService
    {
        PlayerServiceViewModel CreateHumanPlayer(string name);

        PlayerServiceViewModel CreateBot();

        PlayerServiceViewModel CreateDealer();
    }
}
