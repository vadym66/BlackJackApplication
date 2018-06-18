using BlackJackApp.Entities.Entities;
using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services.ServiceInterfaces
{
    public interface IGameService
    {
        Task<RoundViewModel> StartGame(GameServiceViewModel viewFromUI);
        Task<RoundViewModel> StartNextRoundForPlayers(List<UserViewModel> players);
        Task<RoundViewModel> StartNextRoundForDealer(List<UserViewModel> players);
    }
}
