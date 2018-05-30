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
    public class PlayerService : IPlayerService
    {
        IPlayerRepository _playerRepository;

        public PlayerService(IPlayerRepository playerRepository)
        {
            _playerRepository = playerRepository;
        }

        public PlayerServiceViewModel CreateBot()
        {
            var player = new Player { Name = "Bot" };
            _playerRepository.Add(player);
            player = _playerRepository.GetLast();

            var playerServiceViewModel = new PlayerServiceViewModel();

            //Mapping
            playerServiceViewModel.Name = player.Name;
            playerServiceViewModel.PlayerId = player.Id;

            return playerServiceViewModel;

        }

        public PlayerServiceViewModel CreateDealer()
        {
            var player = new Player { Name = "Dealer" };
            _playerRepository.Add(player);
            player = _playerRepository.GetLast();

            var playerServiceViewModel = new PlayerServiceViewModel();

            //Mapping
            playerServiceViewModel.Name = player.Name;
            playerServiceViewModel.PlayerId = player.Id;

            return playerServiceViewModel;
        }

        public PlayerServiceViewModel CreateHumanPlayer(string name)
        {
            var player = new Player { Name = name };
            _playerRepository.Add(player);
            player = _playerRepository.GetLast();

            var playerServiceViewModel = new PlayerServiceViewModel();

            //Mapping
            playerServiceViewModel.Name = player.Name;
            playerServiceViewModel.PlayerId = player.Id;

            return playerServiceViewModel;
        }
    }
}
