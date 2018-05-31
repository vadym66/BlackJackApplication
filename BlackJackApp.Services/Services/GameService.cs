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

        private Game _game;
        private Player _player;
        private Round _round;
        private Card _card1;
        private Card _card2;
        private UserViewModel _userViewModel;

        private List<Player> _listOfPlayers;
        private List<UserViewModel> _listOfUserViewModels;

        public GameService(IGameRepository gameRepository, 
                            IPlayerRepository playerRepository, 
                            IRoundRepository roundRepository,
                            ICardRepository cardRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _roundRepository = roundRepository;
            _cardRepository = cardRepository;

            _game = new Game();

            _listOfPlayers = new List<Player>();
            _listOfUserViewModels = new List<UserViewModel>();
        }

        public List<UserViewModel> CreateGame(GameServiceViewModel gameViewModel) // Creating first round 
        {
            _game = AddGameToDataBase();
            _listOfPlayers = CreatePlayers(gameViewModel.PlayerName, gameViewModel.BotQuantity);
            _listOfUserViewModels = GetCompleteUserViewModel(_listOfPlayers);
            this.AddFirstRoundToDataBase(_listOfUserViewModels, _game.Id);
            CheckForWinner(_listOfUserViewModels);

            return _listOfUserViewModels;
        }
        private Game AddGameToDataBase()
        {
            _gameRepository.Add(_game);
            _game = _gameRepository.GetLast();
            return _game;
        }

        private List<Player> CreatePlayers(string name, int quantityBot)
        {
            _player = new Player { Name = name }; //HumanPlayer Creating
            _playerRepository.Add(_player); //Adding to db
            _listOfPlayers.Add(_playerRepository.GetLast()); //Get from db

            if (quantityBot != 0) // Bot Creating
            {
                for (int i = 0; i < quantityBot; i++)
                {
                    _playerRepository.Add(new Player { Name = $"Bot{i}" }); //Adding to db
                    _listOfPlayers.Add(_playerRepository.GetLast()); //Get from db
                }
            }

            _player = new Player { Name = "Dealer" }; // Dealer Creating
            _playerRepository.Add(_player); //Adding to db
            _listOfPlayers.Add(_playerRepository.GetLast()); //Get from db

            return _listOfPlayers;
        }

        private List<UserViewModel> GetCompleteUserViewModel(List<Player> players)
        {
            for (int i = 0; i < players.Count; i++)
            {
                _userViewModel = new UserViewModel();
                _card1 = _cardRepository.GetRandom();
                _card2 = _cardRepository.GetRandom();

                _userViewModel.GameId = _game.Id;
                _userViewModel.PlayerId = players[i].Id;
                _userViewModel.Name = players[i].Name;

                _userViewModel.FirstCardId = _card1.Id;
                _userViewModel.SecondCardId = _card2.Id;

                _userViewModel.CurrentCard1.CardRank = _card1.CardRank.ToString();
                _userViewModel.CurrentCard2.CardRank = _card2.CardRank.ToString();

                _userViewModel.CurrentCard1.CardSuit = _card1.CardSuit.ToString();
                _userViewModel.CurrentCard2.CardSuit = _card2.CardSuit.ToString();

                _userViewModel.CurrentCard1.CardWeight = _card1.Weight;
                _userViewModel.CurrentCard2.CardWeight = _card2.Weight;
                _userViewModel.SumOfCards = _card1.Weight + _card2.Weight;

                _listOfUserViewModels.Add(_userViewModel);
            }

            return _listOfUserViewModels;
        }

        private void AddFirstRoundToDataBase (List<UserViewModel> players, int gameId)
        {
            for (int i = 0; i < players.Count; i++)
            {
                _round = new Round();

                _round.PlayerId = players[i].PlayerId;
                _round.CardId = players[i].FirstCardId;
                _roundRepository.Add(_round, players[i].GameId);

                _round = new Round();
                _round.PlayerId = players[i].PlayerId;
                _round.CardId = players[i].SecondCardId;
                _roundRepository.Add(_round, players[i].GameId);

            }
        }
        
        private List<UserViewModel> CheckForWinner(List<UserViewModel> userViewModels)
        {
            for (int i = 0; i < userViewModels.Count; i++)
            {
                if (userViewModels[i].CurrentCard1.CardWeight + 
                    userViewModels[i].CurrentCard2.CardWeight == 21)
                {
                    userViewModels[i].IsWinner = true;
                }
            }
            return userViewModels;
        }
    }
}
