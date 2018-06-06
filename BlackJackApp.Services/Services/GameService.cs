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

        public async Task<GameServiceViewModel> CreateGame(GameServiceViewModel viewFromUI) // Creating first round 
        {
            var game = new Game();
            game.Id = await _gameRepository.Add(game);

            viewFromUI.GameId = game.Id;

            return viewFromUI;

            
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
