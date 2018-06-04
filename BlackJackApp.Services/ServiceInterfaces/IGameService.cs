using BlackJackApp.Entities.Entities;
using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services.ServiceInterfaces
{
    public interface IGameService<T> where T : class
    {
        Task<List<UserViewModel>> CreateGame(Task<GameServiceViewModel> viewFromUI);

        Task<Game> AddGameToDataBase();

        Task<List<Player>> CreatePlayers(string name, int quantityBot);

        Task AddFirstRoundToDataBase(List<UserViewModel> players, int gameId);

        Task<List<UserViewModel>> CheckForWinner(List<UserViewModel> userViewModels);
    }
}
