using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services.ServiceInterfaces;
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
            Player playerBot = new Player();
            _playerRepository.Add(playerBot);

            ///PlayerServiceviewModel.MapsFrom(playerBot)
            return new PlayerServiceViewModel();

        }

        public PlayerServiceViewModel CreateDealer()
        {
            throw new NotImplementedException();
        }

        public void CreateHumanPlayer(PlayerServiceViewModel player)
        {
            throw new NotImplementedException();
        }

        public PlayerServiceViewModel TakeCard(CardServiceViewModel card)
        {

        }
    }
}
