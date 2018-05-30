using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services.ServiceInterfaces;
using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services
{
    public class GameService : IGameService
    {
        private IGameRepository _gameRepository;

        public GameService(IGameRepository gameRepository)
        {
            _gameRepository = gameRepository;
        }

        public void CreateGame()
        {
            Game game = new Game();
            _gameRepository.Add(game);
        }

        //public GameServiceViewModel GenerateGameServiceViewModel()
        //{
        //    Game game = _gameRepository.GetLast();

        //    GameServiceViewModel gameServiceViewModel = new GameServiceViewModel();
        //    gameServiceViewModel.GameId = game.Id;

        //    return gameServiceViewModel; 
        //}

        public void GetAllGames()
        {
            _gameRepository.GetAll();
        }

        public void GetLastGame()
        {
            
        }
    }
}
