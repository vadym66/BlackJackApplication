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
    public class GameService : IGameService<Game>
    {
        private IGameRepository<Game> _gameRepository;
        private IRoundRepository<Round> _roundRepository;
        private IPlayerRepository<Player> _playerRepository;
        private ICardRepository<Card> _cardRepository;
        private List<Round> _rounds;

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

            if (humanPlayer.CardSum > 21)
            {
                roundModel.isResultComplete = true;
                FinalPointsCount(); // compare all players to dealer;
            }

            if (humanPlayer.CardSum == 21)
            {
                roundModel.isResultComplete = true;
                FinalPointsCount();
            }

        }

        private bool CheckForBlackJack(RoundViewModel viewModel)
        {
            foreach (var player in viewModel.Users)
            {
                if (player.CardSum == 21)
                {
                    player.winnerFlag = WinnerFlag.isWinner;
                }
            }
                
        }

        public async Task<RoundViewModel> StartNextRoundForPlayers(List<UserViewModel> players)
        {
            foreach (var player in players)
            {
                if (player.PlayerRole == PlayerRole.isHuman ||
                    player.PlayerRole == PlayerRole.isBot &&
                    player.winnerFlag != WinnerFlag.isWinner)
                {
                    await CreateNextRound(player);
                }
            }
            var roundModel = new RoundViewModel();
            roundModel.Users = players;

            return roundModel;
        }

        public async Task StartNextRoundForDealer(List<UserViewModel> players)
        {
            foreach (var player in players)
            {
                if (player.PlayerRole == PlayerRole.isDealer)
                {
                    await CheckDealer(player);
                }
            }

            FinalPointsCount(players);
        }

        private async Task FinalPointsCount(List<UserViewModel> players)
        {
            var humanPlayer = new UserViewModel();
            var dealer = new UserViewModel();

            foreach (var player in players)
            {
                if (player.PlayerRole == PlayerRole.isHuman)
                {
                    humanPlayer = player;
                }
                if (player.PlayerRole == PlayerRole.isDealer)
                {
                    dealer = player;
                }
            }

            if (humanPlayer.CardSum > dealer.CardSum)
            {
                foreach (var player in players)
                {
                    if (player.PlayerRole == PlayerRole.isHuman)
                    {
                        player.winnerFlag = WinnerFlag.isWinner;
                    }
                }
            }
            if (humanPlayer.CardSum < dealer.CardSum)
            {
                foreach (var player in players)
                {
                    if (player.PlayerRole == PlayerRole.isDealer)
                    {
                        player.winnerFlag = WinnerFlag.isWinner;
                    }
                }
            }
            if (humanPlayer.CardSum == dealer.CardSum)
            {
                foreach (var player in players)
                {
                    if (player.PlayerRole == PlayerRole.isHuman)
                    {
                        player.winnerFlag = WinnerFlag.isDraw;
                        dealer.winnerFlag = WinnerFlag.isDraw;
                    }
                }
            }
        }

        private void CheckForMoreThanTwentyOnePoints(UserViewModel player)
        {
            if (player.CardSum > 21)
            {
                player.winnerFlag = WinnerFlag.isNotWinner;
            }
            if (true)
            {

            }
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
            cardView.CardRank = card.CardRank.ToString();
            cardView.CardSuit = card.CardSuit.ToString();

            player.Cards.Add(cardView);
        }

        private async Task CheckDealer(UserViewModel player)
        {
            while (player.CardSum < 17)
            {
                await CreateNextRound(player);
            }
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
            player.PlayerRole = EntityPlayerRole.isHuman;
            player.Id = await _playerRepository.Add(player, gameId);

            await CreateFirstRound(player, gameId);
        }

        private async Task CreateBots(int botNumber, int gameId)
        {
            IEnumerable<Player> players;

            players = await _playerRepository.GetBots(botNumber);

            foreach (var player in players)
            {
                player.PlayerRole = EntityPlayerRole.isBot;
                await CreateFirstRound(player, gameId);
            }
        }

        private async Task CreateDealer(int gameId)
        {
            var dealer = await _playerRepository.GetDealer();
            dealer.PlayerRole = EntityPlayerRole.isDealer;
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

                    cardViewModel.CardRank = item.Card.CardRank.ToString();
                    cardViewModel.CardSuit = item.Card.CardSuit.ToString();
                    userModel.CardSum += item.Card.Weight;
                    userModel.PlayerId = item.PlayerId;
                    userModel.GameId = item.GameId;

                    if (item.Player.PlayerRole == EntityPlayerRole.isHuman)
                    {
                        userModel.PlayerRole = PlayerRole.isHuman;
                    }

                    if (item.Player.PlayerRole == EntityPlayerRole.isBot)
                    {
                        userModel.PlayerRole = PlayerRole.isBot;
                    }

                    if (item.Player.PlayerRole == EntityPlayerRole.isDealer)
                    {
                        userModel.PlayerRole = PlayerRole.isDealer;
                    }

                    userModel.Cards.Add(cardViewModel);
                }
                roundViewModelList.Users.Add(userModel);
            }
            return roundViewModelList;
        }
        
        private void CheckAce(Card card, int playerCardSum)
        {
            if (card.CardRank == CardRank.Ace)
            {
                if (card.Weight + playerCardSum > 21)
                {
                    card.Weight = 1;
                }
            }
        }

        private UserViewModel GetHumanPlayer(List<UserViewModel> players)
        {
            foreach (var player in players)
            {
                if (player.PlayerRole == PlayerRole.isHuman)
                {
                    return player;
                }
            }
            return null;
        }
    }
}
