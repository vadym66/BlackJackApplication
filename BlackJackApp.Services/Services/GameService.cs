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
    public class GameService : IGameService<Game>
    {
        private IGameRepository<Game> _gameRepository;
        private IRoundRepository<Round> _roundRepository;
        private IPlayerRepository<Player> _playerRepository;
        private ICardRepository<Card> _cardRepository;

        public GameService(IGameRepository<Game> gameRepository, 
                            IPlayerRepository<Player> playerRepository, 
                            IRoundRepository<Round> roundRepository,
                            ICardRepository<Card> cardRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _roundRepository = roundRepository;
            _cardRepository = cardRepository;

            _game = new Game();

            _listOfPlayers = new List<Player>();
            _listOfUserViewModels = new List<UserViewModel>();
        }

        public async Task<List<UserViewModel>> CreateGame(Task<GameServiceViewModel> viewFromUI) // Creating first round 
        {
            var game = await AddGameToDataBase();
            var listOfPlayers = await CreatePlayers(viewFromUI.Result.PlayerName, viewFromUI.Result.BotQuantity);

            var listOfUserViewModels = await GetCompleteUserViewModel(listOfPlayers, game);

            await AddFirstRoundToDataBase(listOfUserViewModels, game.Id);
            CheckForWinner(listOfUserViewModels);

            return listOfUserViewModels;
        }

        private async Task<Game> AddGameToDataBase()
        {
            Game game = new Game();
            await _gameRepository.Add(game);
            game = await _gameRepository.GetLast();
            return game;
        }

        private async Task<List<Player>> CreatePlayers(string name, int quantityBot)
        {
            List<Player> listOfPlayers = new List<Player>();

            _player = new Player { Name = name }; //HumanPlayer Creating
            await _playerRepository.Add(_player); //Adding to db
            var player = await _playerRepository.GetLast();
            _listOfPlayers.Add(player); //Get from db

            if (quantityBot != 0) // Bot Creating
            {
                for (int i = 0; i < quantityBot; i++)
                {
                    await _playerRepository.Add(new Player { Name = $"Bot{i}" }); //Adding to db
                    player = await _playerRepository.GetLast();
                    _listOfPlayers.Add(player); //Get from db
                }
            }

            _player = new Player { Name = "Dealer" }; // Dealer Creating
            await _playerRepository.Add(_player); //Adding to db
            player = await _playerRepository.GetLast();
            listOfPlayers.Add(player); //Get from db

            return listOfPlayers;
        }

        private async Task<List<UserViewModel>> GetCompleteUserViewModel(List<Player> players, Game game)
        {
            List<UserViewModel> listOfUserViewModel = new List<UserViewModel>();

            for (int i = 0; i < players.Count; i++)
            {
                var userViewModel = new UserViewModel();

                Card card1 = await _cardRepository.GetRandom();
                Card card2 = await _cardRepository.GetRandom();

                userViewModel.GameId = game.Id;
                userViewModel.PlayerId = players[i].Id;
                userViewModel.Name = players[i].Name;

                userViewModel.CurrentCard.Add(new CardServiceViewModel());
                userViewModel.CurrentCard.Add(new CardServiceViewModel());

                userViewModel.CurrentCard[0].CardId = card1.Id;
                userViewModel.CurrentCard[0].CardRank = card1.CardRank.ToString();
                userViewModel.CurrentCard[0].CardSuit = card1.CardSuit.ToString();
                userViewModel.CurrentCard[0].CardWeight = card1.Weight;

                userViewModel.CurrentCard[1].CardId = card2.Id;
                userViewModel.CurrentCard[1].CardSuit = card2.CardSuit.ToString();
                userViewModel.CurrentCard[1].CardRank = card2.CardRank.ToString();
                userViewModel.CurrentCard[1].CardWeight = card2.Weight;

                userViewModel.SumOfCards = card1.Weight + card2.Weight;

                listOfUserViewModel.Add(userViewModel);
            }

            return listOfUserViewModel;
        }

        private async Task AddFirstRoundToDataBase (List<UserViewModel> players, int gameId)
        {
            for (int i = 0; i < players.Count; i++)
            {
                var round = new Round();

                round.PlayerId = players[i].PlayerId;
                round.CardId = players[i].CurrentCard[0].CardId;
                await _roundRepository.Add(round, players[i].GameId);

                round = new Round();
                round.PlayerId = players[i].PlayerId;
                round.CardId = players[i].CurrentCard[1].CardId;
                await _roundRepository.Add(round, players[i].GameId);
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
