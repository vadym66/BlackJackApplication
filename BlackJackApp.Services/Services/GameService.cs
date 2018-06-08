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

            return MappingToViewModel(_rounds);
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
            player.Id = await _playerRepository.Add(player, gameId);

            await CreateFirstRound(player, gameId);
        }

        private async Task CreateBots(int botNumber, int gameId)
        {
            IEnumerable<Player> players;

            players = await _playerRepository.GetBots(botNumber);
            foreach (var player in players)
            {
                await CreateFirstRound(player, gameId);
            }
        }

        private async Task CreateDealer(int gameId)
        {
            var dealer = await _playerRepository.GetDealer();
            await CreateFirstRound(dealer, gameId);
        }

        private async Task CreateFirstRound(Player player,int gameId)
        {
            var firstRound = new Round();
            var secondRound = new Round();

            var firstCard = new Card();
            var secondCard = new Card();

            firstCard = await _cardRepository.GetRandom();
            secondCard = await _cardRepository.GetRandom();

            firstRound.PlayerId = player.Id;
            firstRound.CardId = firstCard.Id;
            firstRound.Id = await _roundRepository.Add(firstRound, gameId);

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

                foreach (var card in round)
                {
                    var cardViewModel = new CardServiceViewModel();

                    cardViewModel.CardRank = card.Card.CardRank.ToString();
                    cardViewModel.CardSuit = card.Card.CardSuit.ToString();

                    userModel.Cards.Add(cardViewModel);
                }
                roundViewModelList.Users.Add(userModel);
            }
            return roundViewModelList;
        }
    }
}
