using BlackJackApp.DAL.Interfaces;
using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Entities.Enums;
using BlackJackApp.Services.Enums;
using BlackJackApp.Services.ServiceInterfaces;
using BlackJackApp.Services.Services;
using BlackJackApp.ViewModels;
using BlackJackApp.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services
{
    public class GameService : IGameService<GameService>
    {
        private IGameRepository<Game> _gameRepository;
        private IRoundRepository<Round> _roundRepository;
        private IPlayerRepository<Player> _playerRepository;
        private ICardRepository<Card> _cardRepository;
        private IPlayersGameRepository<Player> _playersGameRepository;
        private List<Round> _rounds;
        private const int twentyOnePoint = 21;
        private const int dealerPointBorder = 17;

        public GameService(IGameRepository<Game> gameRepository,
                            IPlayerRepository<Player> playerRepository,
                            IRoundRepository<Round> roundRepository,
                            ICardRepository<Card> cardRepository,
                            IPlayersGameRepository<Player> playersGameRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _roundRepository = roundRepository;
            _cardRepository = cardRepository;
            _playersGameRepository = playersGameRepository;

            _rounds = new List<Round>();
        }

        public async Task<RoundViewModel> StartGame(GameServiceViewModel viewFromUI) // Creating first round 
        {
            var gameId = await CreateGame(viewFromUI.PlayerName, viewFromUI.BotQuantity);
            await CreateHuman(viewFromUI.PlayerName, gameId);
            if (viewFromUI.BotQuantity != 0)
            {
                await CreateBots(viewFromUI.BotQuantity, gameId);
            }
            await CreateDealer(gameId);
            var roundModel = MappingToViewModel(_rounds);

            await CheckPlayersForMoreThanTwentyOne(roundModel);
            var humanPlayer = GetHumanPlayer(roundModel.Users);
            var dealer = GetDealer(roundModel.Users);
            if (humanPlayer.PlayerStatus == PlayerStatus.Lose)
            {
                await CheckDealer(roundModel.Users);
                await FinalPointsCount(roundModel.Users);
                roundModel.isResultComplete = true;
                return roundModel;
            }
            if (dealer.PlayerStatus == PlayerStatus.Lose)
            {
                return await FinalPointsCount(roundModel.Users);
            }

            await CheckPlayersForBlackJack(roundModel);
            humanPlayer = GetHumanPlayer(roundModel.Users);
            dealer = GetDealer(roundModel.Users);
            if (humanPlayer.PlayerStatus == PlayerStatus.Winner)
            {
                await CheckDealer(roundModel.Users);
                return await FinalPointsCount(roundModel.Users);
            }

            return roundModel;
        }

        public async Task<RoundViewModel> StartNextRoundForPlayers(List<UserViewModel> players)
        {
            foreach (var player in players)
            {
                if (player.PlayerRole != PlayerRole.Dealer &&
                    player.PlayerStatus == PlayerStatus.DefaultValue)
                {
                    await CreateNextRound(player);
                }
                if (player.PlayerRole == PlayerRole.Dealer)
                {
                    List<Round> rounds = await _roundRepository.GetRoundsForPlayer(player.GameId, player.UserName);
                    AddCardViewModelToPlayer(rounds, player);
                }
            }
            var roundModel = new RoundViewModel();
            roundModel.Users = players;

            await CheckPlayersForMoreThanTwentyOne(roundModel);
            var humanPlayer = GetHumanPlayer(roundModel.Users);
            var dealer = GetDealer(roundModel.Users);
            if (humanPlayer.PlayerStatus == PlayerStatus.Lose)
            {
                await CheckDealer(roundModel.Users);
                return await FinalPointsCount(roundModel.Users);
            }
            return roundModel;
        }

        public async Task<RoundViewModel> StartNextRoundForDealer(List<UserViewModel> players)
        {
            await CheckDealer(players);
            return await FinalPointsCount(players);
        }

        private async Task<RoundViewModel> FinalPointsCount(List<UserViewModel> players)
        {
            var roundModel = new RoundViewModel();
            var dealer = GetDealer(players);
            var humanPlayer = GetHumanPlayer(players);

            foreach (var player in players)
            {
                if (player.PlayerStatus == PlayerStatus.DefaultValue)
                {
                    if (player.CardSum > dealer.CardSum)
                    {
                        player.PlayerStatus = PlayerStatus.Winner;
                        dealer.PlayerStatus = PlayerStatus.Lose;
                    }
                    if (player.CardSum < dealer.CardSum)
                    {
                        dealer.PlayerStatus = PlayerStatus.Winner;
                        player.PlayerStatus = PlayerStatus.Lose;
                    }
                }
                if (player.PlayerRole == PlayerRole.Dealer)
                {
                    player.PlayerStatus = dealer.PlayerStatus;
                }
            }
            roundModel.Users = players;
            roundModel.isResultComplete = true;
            return roundModel;
        }

        private async Task<int> CreateGame(string name, int botNumber)
        {
            var game = new Game();
            game.Id = await _gameRepository.Add(game);
            return game.Id;
        }

        private async Task CreateHuman(string name, int gameId)
        {
            Player player = new Player { Name = name };
            player.PlayerRole = EntityPlayerRole.Human;
            player.Id = await _playerRepository.Add(player, gameId);

            await AddPlayerToCurrentGame(player, gameId);
            await CreateFirstRound(player, gameId);
        }

        private async Task CreateBots(int botNumber, int gameId)
        {
            IEnumerable<Player> players;

            players = await _playerRepository.GetBots(botNumber);

            foreach (var player in players)
            {
                await AddPlayerToCurrentGame(player, gameId);
                player.PlayerRole = EntityPlayerRole.Bot;
                await CreateFirstRound(player, gameId);
            }
        }

        private async Task CreateDealer(int gameId)
        {
            var dealer = await _playerRepository.GetDealer();
            dealer.PlayerRole = EntityPlayerRole.Dealer;
            await AddPlayerToCurrentGame(dealer, gameId);
            await CreateFirstRound(dealer, gameId);
        }

        private async Task CreateFirstRound(Player player, int gameId)
        {
            var firstRound = new Round();
            var secondRound = new Round();

            var firstCard = new Card();
            var secondCard = new Card();

            firstCard = await _cardRepository.GetRandom();
            secondCard = await _cardRepository.GetRandom();

            firstRound.GameId = gameId;
            firstRound.PlayerId = player.Id;
            firstRound.CardId = firstCard.Id;
            firstRound.Id = await _roundRepository.Add(firstRound, gameId);

            secondRound.GameId = gameId;
            secondRound.PlayerId = player.Id;
            secondRound.CardId = secondCard.Id;
            secondRound.Id = await _roundRepository.Add(secondRound, gameId);

            firstRound.Card = firstCard;
            firstRound.Player = player;
            _rounds.Add(firstRound);

            secondRound.Card = secondCard;
            secondRound.Player = player;
            _rounds.Add(secondRound);
        }

        private async Task CreateNextRound(UserViewModel player)
        {
            if (player.PlayerRole != PlayerRole.Dealer)
            {
                List<Round> rounds = await _roundRepository.GetRoundsForPlayer(player.GameId, player.UserName);
                AddCardViewModelToPlayer(rounds, player);
            }

            var round = new Round();
            var card = new Card();

            card = await _cardRepository.GetRandom();

            CheckAce(card, player.CardSum);

            round.GameId = player.GameId;
            round.PlayerId = player.PlayerId;
            round.CardId = card.Id;

            await _roundRepository.Add(round, round.GameId);

            var cardView = new CardServiceViewModel();
            cardView.CardRank = card.Rank.ToString();
            cardView.CardSuit = card.Suit.ToString();
            cardView.CardWeight = card.Weight;

            player.CardSum += cardView.CardWeight;

            player.Cards.Add(cardView);
        }

        private void CheckAce(Card card, int playerCardSum)
        {
            if (card.Rank == CardRank.Ace)
            {
                if (card.Weight + playerCardSum > twentyOnePoint)
                {
                    card.Weight = 1;
                }
            }
        }

        private async Task CheckDealer(List<UserViewModel> players)
        {
            foreach (var player in players)
            {
                if (player.PlayerRole == PlayerRole.Dealer)
                {
                    if (player.CardSum < dealerPointBorder)
                    {
                        while (player.CardSum < dealerPointBorder)
                        {
                            await CreateNextRound(player);
                        }
                    }
                    if (player.CardSum > 21)
                    {
                        player.PlayerStatus = PlayerStatus.Lose;
                    }
                    player.PlayerStatus = PlayerStatus.Winner;
                }
            }
        }

        private async Task CheckPlayersForMoreThanTwentyOne(RoundViewModel roundModel)
        {
            var dealer = GetDealer(roundModel.Users);

            foreach (var player in roundModel.Users)
            {
                if (player.CardSum > twentyOnePoint)
                {
                    player.PlayerStatus = PlayerStatus.Lose;
                }
            }
        }

        private async Task<RoundViewModel> CheckPlayersForBlackJack(RoundViewModel viewModel)
        {
            foreach (var player in viewModel.Users)
            {
                if (player.CardSum == twentyOnePoint)
                {
                    player.PlayerStatus = PlayerStatus.Winner;
                }
            }
            return viewModel;
        }

        private UserViewModel GetHumanPlayer(List<UserViewModel> players)
        {
            foreach (var player in players)
            {
                if (player.PlayerRole == PlayerRole.Human)
                {
                    return player;
                }
            }
            return null;
        }

        private UserViewModel GetDealer(List<UserViewModel> players)
        {
            foreach (var player in players)
            {
                if (player.PlayerRole == PlayerRole.Dealer)
                {
                    return player;
                }
            }
            return null;
        }

        private RoundViewModel MappingToViewModel(List<Round> rounds)
        {
            var result = rounds.GroupBy(p => p.Player.Name);

            var roundViewModelList = new RoundViewModel();

            foreach (var round in result)
            {
                var userModel = new UserViewModel();
                userModel.UserName = round.Key;

                foreach (var item in round)
                {
                    var cardViewModel = new CardServiceViewModel();

                    cardViewModel.CardRank = item.Card.Rank.ToString();
                    cardViewModel.CardSuit = item.Card.Suit.ToString();
                    cardViewModel.CardWeight = item.Card.Weight;

                    userModel.CardSum += item.Card.Weight;
                    userModel.PlayerId = item.PlayerId;
                    userModel.GameId = item.GameId;

                    if (item.Player.PlayerRole == EntityPlayerRole.Human)
                    {
                        userModel.PlayerRole = PlayerRole.Human;
                    }

                    if (item.Player.PlayerRole == EntityPlayerRole.Bot)
                    {
                        userModel.PlayerRole = PlayerRole.Bot;
                    }

                    if (item.Player.PlayerRole == EntityPlayerRole.Dealer)
                    {
                        userModel.PlayerRole = PlayerRole.Dealer;
                    }

                    userModel.Cards.Add(cardViewModel);
                }
                roundViewModelList.Users.Add(userModel);
            }
            return roundViewModelList;
        }

        private async Task AddPlayerToCurrentGame(Player player, int gameId)
        {
            await _playersGameRepository.AddPlayer(player, gameId);
        }

        private void AddCardViewModelToPlayer(List<Round> rounds, UserViewModel player)
        {
            foreach (var cardItem in rounds)
            {
                var cardView = new CardServiceViewModel();
                cardView.CardRank = cardItem.Card.Rank.ToString();
                cardView.CardSuit = cardItem.Card.Suit.ToString();
                cardView.CardWeight = cardItem.Card.Weight;
                player.CardSum += cardView.CardWeight;
                player.Cards.Add(cardView);
            }
        }

        //private async Task AddPlayerStatusToCurrentGame(UserViewModel user)
        //{
        //    var player = new Player();
        //    player.Id = user.PlayerId;
        //    player.Name = user.UserName;
        //    var playerStatus = user.PlayerStatus.ToString();
        //    await _playersGameRepository.AddPlayerStatus(player, playerStatus);
        //}
    }
}
