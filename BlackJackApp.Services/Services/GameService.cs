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
        private List<Round> _rounds;
        private const int twentyOneBlackJack = 21;

        public GameService(IGameRepository<Game> gameRepository,
                            IPlayerRepository<Player> playerRepository,
                            IRoundRepository<Round> roundRepository,
                            ICardRepository<Card> cardRepository)
        {
            _gameRepository = gameRepository;
            _playerRepository = playerRepository;
            _roundRepository = roundRepository;
            _cardRepository = cardRepository;

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

            var humanPlayer = GetHumanPlayer(roundModel.Users);
            var dealer = GetDealer(roundModel.Users);

            CheckForBlackJack(roundModel);

            CheckBotsForMoreThanTwentyOne(roundModel.Users);

            if (humanPlayer.CardSum > twentyOneBlackJack)
            {
                roundModel.isResultComplete = true;
                humanPlayer.PlayerStatus = PlayerStatus.Lose;
                await CheckDealer(dealer);
                await FinalPointsCount(roundModel.Users); // compare all players to dealer;
            }

            if (humanPlayer.CardSum == twentyOneBlackJack)
            {
                roundModel.isResultComplete = true;
                humanPlayer.PlayerStatus = PlayerStatus.Winner;
                foreach (var player in roundModel.Users)
                {
                    if (player.PlayerRole == PlayerRole.Bot ||
                        player.PlayerRole == PlayerRole.Dealer)
                    {
                        player.PlayerStatus = PlayerStatus.Lose;
                    }
                }
            }
            return roundModel;
        }

        public async Task<RoundViewModel> StartNextRoundForPlayers(List<UserViewModel> players)
        {
            foreach (var player in players)
            {
                if (player.PlayerRole == PlayerRole.Human ||
                    player.PlayerRole == PlayerRole.Bot &&
                    player.PlayerStatus != PlayerStatus.Winner)
                {
                    await CreateNextRound(player);
                }
            }
            var roundModel = new RoundViewModel();
            roundModel.Users = players;

            var humanPlayer = GetHumanPlayer(roundModel.Users);
            if (humanPlayer.CardSum > twentyOneBlackJack)
            {
                roundModel.isResultComplete = true;
                return await FinalPointsCount(players); // compare all players to dealer;
            }
            return roundModel;
        }

        public async Task StartNextRoundForDealer(List<UserViewModel> players)
        {
            foreach (var player in players)
            {
                if (player.PlayerRole == PlayerRole.Dealer)
                {
                    await CheckDealer(player);
                }
            }

            FinalPointsCount(players);
        }

        private async Task<RoundViewModel> FinalPointsCount(List<UserViewModel> players)
        {
            var roundModel = new RoundViewModel();
            var dealer = GetDealer(players);
            var humanPlayer = GetHumanPlayer(players);

            if (dealer.CardSum == humanPlayer.CardSum)
            {
                roundModel.isResultComplete = true;
                foreach (var player in players)
                {
                    if (player.PlayerRole == PlayerRole.Human ||
                        player.PlayerRole == PlayerRole.Dealer)
                    {
                        player.PlayerStatus = PlayerStatus.Draw;
                    }
                }
                roundModel.Users = players;
                return roundModel;
            }

            foreach (var player in players)
            {
                if (player.PlayerStatus == PlayerStatus.DefaultValue &&
                    player.PlayerRole == PlayerRole.Human ||
                    player.PlayerRole == PlayerRole.Bot)
                {
                    if (player.CardSum > dealer.CardSum)
                    {
                        player.PlayerStatus = PlayerStatus.Winner;
                    }
                    else
                    {
                        player.PlayerStatus = PlayerStatus.Lose;
                    }
                }
            }
            roundModel.isResultComplete = true;
            roundModel.Users = players;
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

            await CreateFirstRound(player, gameId);
        }

        private async Task CreateBots(int botNumber, int gameId)
        {
            IEnumerable<Player> players;

            players = await _playerRepository.GetBots(botNumber);

            foreach (var player in players)
            {
                player.PlayerRole = EntityPlayerRole.Bot;
                await CreateFirstRound(player, gameId);
            }
        }

        private async Task CreateDealer(int gameId)
        {
            var dealer = await _playerRepository.GetDealer();
            dealer.PlayerRole = EntityPlayerRole.Dealer;
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
            var round = new Round();
            var card = new Card();

            card = await _cardRepository.GetRandom();

            CheckAce(card, player.CardSum);

            round.GameId = player.GameId;
            round.PlayerId = player.PlayerId;
            round.CardId = card.Id;

            await _roundRepository.Add(round, round.GameId);

            player.CardSum += card.Weight;

            var cardView = new CardServiceViewModel();
            cardView.CardRank = card.Rank.ToString();
            cardView.CardSuit = card.Suit.ToString();

            player.Cards.Add(cardView);
        }

        private void CheckAce(Card card, int playerCardSum)
        {
            if (card.Rank == CardRank.Ace)
            {
                if (card.Weight + playerCardSum > twentyOneBlackJack)
                {
                    card.Weight = 1;
                }
            }
        }

        private async Task CheckDealer(UserViewModel player)
        {
            while (player.CardSum < 17)
            {
                await CreateNextRound(player);
            }
        }

        private void CheckBotsForMoreThanTwentyOne(List<UserViewModel> players)
        {
            foreach (var player in players)
            {
                if (player.PlayerRole == PlayerRole.Bot &&
                    player.CardSum > twentyOneBlackJack)
                {
                    player.PlayerStatus = PlayerStatus.Lose;
                }
            }
        }

        private RoundViewModel CheckForBlackJack(RoundViewModel viewModel)
        {
            foreach (var player in viewModel.Users)
            {
                if (player.CardSum == twentyOneBlackJack)
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
    }
}
