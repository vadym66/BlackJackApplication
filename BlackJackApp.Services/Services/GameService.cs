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
        private IRoundRepository _roundRepository;
        private IPlayerRepository _playerRepository;
        private ICardRepository _cardRepository;
        private Player _player;
        private List<Player> _players;
        private Round _round;
        private Card _card;
        private Game _game;
        private List<RoundServiceViewModel> _roundServiceViewModelsList;

        public GameService(IGameRepository gameRepository, 
                            IPlayerRepository playerRepository, 
                            IRoundRepository roundRepository,
                            ICardRepository cardRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _roundRepository = roundRepository;
            _cardRepository = cardRepository;
            _players = new List<Player>();
            _game = new Game();
        }

        public GameServiceViewModel CreateGame(GameServiceViewModel gameViewModel)
        {            
            _gameRepository.Add(_game);
            _game = _gameRepository.GetLast();

            CreateHuman(gameViewModel.PlayerName);
            var human = _playerRepository.GetLast();
            _players.Add(human);

            if (gameViewModel.BotQuantity != 0)
            {
                CreateBot(gameViewModel.BotQuantity);
                _players.AddRange(_playerRepository.GetSequence(gameViewModel.BotQuantity));
            }

            CreateDealer();
            var dealer = _playerRepository.GetLast();
            _players.Add(dealer);



            CreateRound();

            
            
            var gameServiceViewModel = new GameServiceViewModel();
            return gameServiceViewModel;
        }
        

        private void CreateRound(List<Player> players)
        {
            for (int i = 0; i < _players.Count(); i++)
            {
                _round = new Round();
                _card = _cardRepository.GetRandom();

                _round.CardId = _card.Id;
                _round.PlayerId = _players[i].Id;
                _roundRepository.Add(_round, _game.Id);
                _roundServiceViewModelsList.Add(new RoundServiceViewModel { Name = _players[i].Name,
                                                                            new CardServiceViewModel {CardRank =  } = })
            }
                
            
        }

        private void CreateBot(int? quantityBot)
        {
            for (int i = 0; i < quantityBot; i++)
            {
                _playerRepository.Add(new Player { Name = $"Bot{1}" });
            }
        }

        private void CreateHuman(string name)
        {
            _player = new Player{Name = name};
            _playerRepository.Add(_player);
        }

        private void CreateDealer()
        {
            _player = new Player { Name = "Dealer"};
            _playerRepository.Add(_player);
        }

        
    }
}
