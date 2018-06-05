using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services.ServiceInterfaces;
using BlackJackApp.Services.Services;
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

        private List<Player> listOfPlayers = new List<Player>();

        public GameService(IGameRepository<Game> gameRepository,
                            IPlayerRepository<Player> playerRepository,
                            IRoundRepository<Round> roundRepository,
                            ICardRepository<Card> cardRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _roundRepository = roundRepository;
            _cardRepository = cardRepository;
        }

        public async Task<List<UserViewModel>> CreateGame(GameServiceViewModel viewFromUI) // Creating first round 
        {
            //var playerHuman = await CreateHumanPlayer();
            //var playerDealer = await CreateDealerPayer();
            //var playerBots = await CreatePlayerBots();

            //var list = new List<Player>();
            //list.Add(playerHuman);
            //list.Add(playerDealer);
            //list.AddRange(playerBots);

            var game = await AddGameToDataBase();

            var listOfPlayers = await CreatePlayers();

            var userViewModels = await GetCompleteUserViewModel(listOfPlayers, game);

            //await AddFirstRoundToDataBase(userViewModels, game.Id);

            foreach (var item in userViewModels)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("===============DIFERENCE===============");

            return userViewModels;
        }

        private async Task<Game> AddGameToDataBase()
        {
            var game = new Game();
            game.Id = await _gameRepository.Add(game);
            return game;
        }

        private async Task<List<Player>> CreatePlayers()
        {
            var playerHuman = await CreateHumanPlayer();
            var playerDealer = await CreateDealerPayer();
            var playerBots = await CreatePlayerBots();

            var list = new List<Player>();
            list.Add(playerHuman);
            list.Add(playerDealer);
            list.AddRange(playerBots);

            return list;
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

        private async Task AddFirstRoundToDataBase(List<UserViewModel> players, int gameId)
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
            var dealer = userViewModels[userViewModels.Count - 1];

            if (dealer.SumOfCards == 21)
            {
                dealer.IsWinner = Enums.WinnerFlag.winner;
                return userViewModels;
            }

            for (int i = 0; i < userViewModels.Count - 1; i++)
            {
                if (dealer.SumOfCards == userViewModels[i].SumOfCards &&
                    dealer.SumOfCards == 21 &&
                    userViewModels[i].SumOfCards == 21)
                {
                    userViewModels[i].IsWinner = Enums.WinnerFlag.draw;
                }

                if (userViewModels[i].SumOfCards == 21)
                {
                    userViewModels[i].IsWinner = Enums.WinnerFlag.winner;
                }

                if (userViewModels[0].SumOfCards > 21)
                {
                    FinalCount(userViewModels);
                    return userViewModels;
                }
            }
            return userViewModels;
        }

        private List<UserViewModel> FinalCount(List<UserViewModel> userViewModels)
        {
            userViewModels.Sort(new ComparerForUserView());
            userViewModels[userViewModels.Count - 1].IsWinner = Enums.WinnerFlag.winner;
            return userViewModels;
        }

        private async Task<Player> CreateHumanPlayer(string name = "Human")
        {
            var player = new Player { Name = name }; //HumanPlayer Creating
            player.Id = await _playerRepository.Add(player);

            return player;
        }

        private async Task<Player> CreateDealerPayer()
        {
            var playerDealer = new Player { Name = "Dealer" }; // Dealer Creating
            playerDealer.Id = await _playerRepository.Add(playerDealer);
            listOfPlayers.Add(playerDealer);

            return playerDealer;
        }

        private async Task<List<Player>> CreatePlayerBots(int quantityBot = 2)
        {
            var listOfPlayers = new List<Player>();

            if (quantityBot != 0) // Bot Creating
            {
                for (int i = 0; i < quantityBot; i++)
                {
                    var playerBot = new Player { Name = $"Bot{i}" };
                    playerBot.Id = await _playerRepository.Add(playerBot);

                    listOfPlayers.Add(playerBot);
                }
            }
            return listOfPlayers;
        }




    }
}
