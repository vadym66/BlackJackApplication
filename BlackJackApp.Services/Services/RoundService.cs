using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services.ServiceInterfaces;
using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Configuration;
using System.Threading.Tasks;
using BlackJackApp.Services.Services;

namespace BlackJackApp.Services
{
    public class RoundService : IRoundService
    {
        private IRoundRepository<Round> _roundRepository;
        private ICardRepository<Card> _cardRepository;
        private IGameRepository<Game> _gameRepository;
        private IPlayerRepository<Player> _playerRepository;

        private List<Round> _rounds = new List<Round>();

        public RoundService(IRoundRepository<Round> roundRepository, ICardRepository<Card> cardRepository, IGameRepository<Game> gameRepository, IPlayerRepository<Player> playerRepository)
        {
            _roundRepository = roundRepository;
            _cardRepository = cardRepository;
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
        }

        public async Task<List<UserViewModel>> CreateFirstRound(GameServiceViewModel gameServiceVM)
        {
            var listOfPlayers = await CreatePlayers(gameServiceVM.BotQuantity, gameServiceVM.PlayerName);

            var userViewModels = await GetCompleteUserViewModel(listOfPlayers, gameServiceVM);

            await AddFirstRoundToDataBase(userViewModels, gameServiceVM.GameId);

            CheckForWinner(userViewModels);


            foreach (var item in _rounds)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("=============RoundINFO==============");


            //await AddFirstRoundToDataBase(userViewModels, game.Id);


            foreach (var item in userViewModels)
            {
                Console.WriteLine(item.ToString());
                foreach (var card in item.CurrentCard)
                {
                    Console.WriteLine(card.ToString());
                }
            }
            Console.WriteLine("===============DIFERENCE===============");

            return userViewModels;
        }

        public async Task<List<UserViewModel>> CreateNextRound(List<UserViewModel> listOfUserViewModels)
        {
            for (int i = 0; i < listOfUserViewModels.Count - 1; i++)
            {
                var card = new Card();
                card = await _cardRepository.GetRandom();

                listOfUserViewModels[i].CurrentCard.Add(new CardServiceViewModel());
                int lastCard = listOfUserViewModels[i].CurrentCard.Count - 1;

                listOfUserViewModels[i].CurrentCard[lastCard].CardRank = card.CardRank.ToString();
                listOfUserViewModels[i].CurrentCard[lastCard].CardSuit = card.CardSuit.ToString();
                listOfUserViewModels[i].CurrentCard[lastCard].CardId = card.Id;
                listOfUserViewModels[i].SumOfCards += card.Weight;

                await AddNextRoundToDataBase(listOfUserViewModels[i], listOfUserViewModels[0].GameId);
            }
            foreach (var item in _rounds)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("=============RoundINFO==============");

            CheckForWinner(listOfUserViewModels);

            return listOfUserViewModels;
        }

        public async Task<List<UserViewModel>> TotalCount(List<UserViewModel> listOfUserViewModels)
        {
            await CheckDealer(listOfUserViewModels[listOfUserViewModels.Count - 1]);
            CheckForWinner(listOfUserViewModels);

            for (int i = 0; i < listOfUserViewModels.Count - 1; i++)
            {
                if (listOfUserViewModels[i].IsWinner == Enums.WinnerFlag.winner)
                {
                    return listOfUserViewModels;
                }
            }

            CountCards(listOfUserViewModels);

            return listOfUserViewModels;
        }

        private List<UserViewModel> CountCards(List<UserViewModel> listOfUserViewModels)
        {
            listOfUserViewModels.Sort(new ComparerForUserView());
            listOfUserViewModels[listOfUserViewModels.Count - 1].IsWinner = Enums.WinnerFlag.winner;
            return listOfUserViewModels;
        }

        private async Task<List<Player>> CreatePlayers(int bots, string name)
        {
            var playerHuman = await CreateHumanPlayer(name);
            var playerDealer = await CreateDealerPayer();
            var playerBots = await CreatePlayerBots(bots);

            var list = new List<Player>();
            list.Add(playerHuman);
            list.AddRange(playerBots);
            list.Add(playerDealer);

            return list;
        }

        private async Task<Player> CreateHumanPlayer(string name = "Human")
        {
            var player = new Player { Name = name };
            player.Id = await _playerRepository.Add(player);

            return player;
        }

        private async Task<List<Player>> CreatePlayerBots(int quantityBot)
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

        private async Task<Player> CreateDealerPayer()
        {
            var playerDealer = new Player { Name = "Dealer" }; // Dealer Creating
            playerDealer.Id = await _playerRepository.Add(playerDealer);

            return playerDealer;
        }

        private async Task<List<UserViewModel>> GetCompleteUserViewModel(List<Player> players, GameServiceViewModel game)
        {
            List<UserViewModel> listOfUserViewModel = new List<UserViewModel>();

            for (int i = 0; i < players.Count; i++)
            {
                var userViewModel = new UserViewModel();

                Card card1 = await _cardRepository.GetRandom();
                Card card2 = await _cardRepository.GetRandom();

                userViewModel.GameId = game.GameId;
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
                var firstRound = new Round();

                firstRound.PlayerId = players[i].PlayerId;
                firstRound.CardId = players[i].CurrentCard[0].CardId;
                firstRound.Id = await _roundRepository.Add(firstRound, players[i].GameId);
                _rounds.Add(firstRound);

                firstRound = new Round();
                firstRound.PlayerId = players[i].PlayerId;
                firstRound.CardId = players[i].CurrentCard[1].CardId;
                firstRound.Id = await _roundRepository.Add(firstRound, players[i].GameId);
                _rounds.Add(firstRound);
            }
        }

        private async Task AddNextRoundToDataBase(UserViewModel player, int gameId)
        {
            var nextRound = new Round();

            nextRound.PlayerId = player.PlayerId;
            nextRound.CardId = player.CurrentCard[player.CurrentCard.Count - 1].CardId;
            nextRound.Id = await _roundRepository.Add(nextRound, player.GameId);
            _rounds.Add(nextRound);
        }

        private async Task<UserViewModel> CreateRoundForDealer(UserViewModel dealer)
        {
            var card = new Card();
            card = await _cardRepository.GetRandom();

            var round = new Round();
            round.PlayerId = dealer.PlayerId;
            round.CardId = card.Id;
            await _roundRepository.Add(round, dealer.GameId);

            dealer.CurrentCard.Add(new CardServiceViewModel());
            int lastCard = dealer.CurrentCard.Count - 1;

            dealer.CurrentCard[lastCard].CardRank = card.CardRank.ToString();
            dealer.CurrentCard[lastCard].CardSuit = card.CardSuit.ToString();
            dealer.SumOfCards += card.Weight;

            return dealer;
        }

        private async Task<UserViewModel> CheckDealer(UserViewModel dealer)
        {
            if (dealer.SumOfCards < 17)
            {
                do
                {
                    dealer = await CreateRoundForDealer(dealer);
                }
                while (dealer.SumOfCards < 17);
            }

            return dealer;
        }

        private List<UserViewModel> CheckForWinner(List<UserViewModel> userViewModels)
        {

            for (int i = 0; i < userViewModels.Count -1; i++)
            {
                if (userViewModels[0].SumOfCards > 21)
                {
                    userViewModels[userViewModels.Count - 1].IsWinner = Enums.WinnerFlag.winner;
                    return userViewModels;
                }

                if (userViewModels[i].SumOfCards == 21)
                {
                    userViewModels[i].IsWinner = Enums.WinnerFlag.winner;
                    return userViewModels;
                }

                if (userViewModels[userViewModels.Count - 1].SumOfCards == 21)
                {
                    userViewModels[userViewModels.Count - 1].IsWinner = Enums.WinnerFlag.winner;
                    return userViewModels;
                }

                if (userViewModels[i].SumOfCards == userViewModels[userViewModels.Count - 1].SumOfCards)
                {
                    userViewModels[userViewModels.Count - 1].IsWinner = Enums.WinnerFlag.draw;
                    return userViewModels;
                }
            }

            return userViewModels;
        }
    }
}