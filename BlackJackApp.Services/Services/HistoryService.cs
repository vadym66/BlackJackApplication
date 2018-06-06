using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services.Services
{
    public class HistoryService
    {
        private IRoundRepository<Round> _roundRepository;
        private IGameRepository<Game> _gameRepository;
        private IPlayerRepository<Player> _playerRepository;
        private ICardRepository<Card> _cardRepository;

        public HistoryService(IRoundRepository<Round> roundRepository, IGameRepository<Game> gameRepository, 
                                IPlayerRepository<Player> playerRepository, ICardRepository<Card> cardRepository)
        {
            _roundRepository = roundRepository;
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _cardRepository = cardRepository;
        }

        public HistoryServiceViewModel GetLastGame()
        {
            return new HistoryServiceViewModel();
        }
    }
}
