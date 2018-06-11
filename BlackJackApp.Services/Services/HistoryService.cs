using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services.Services
{
    public class HistoryService
    {
        private IGameRepository<Game> _gameRepository;
        private IRoundRepository<Round> _roundRepository;

        public HistoryService(IGameRepository<Game> gameRepository, IRoundRepository<Round> roundRepository)
        {
            _gameRepository = gameRepository;
            _roundRepository = roundRepository;
        }

        public async Task<IEnumerable<Round>> GetAllRoundsFromParticularGame(int gameId)
        {
            var query = await _roundRepository.GetRounds(gameId);
            if (!query.Any())
            {
                throw new Exception("There is no such game");
            }

            return query;
        }

        public List<UserHistoryViewModel> CreateUserHistoryVM(IEnumerable<Round> rounds)
        {
            var result = rounds.GroupBy(p => p.Player.Name);

            var userModelList = new List<UserHistoryViewModel>();

            foreach (var round in result)
            {
                var userModel = new UserHistoryViewModel();
                userModel.UserName = round.Key;

                foreach (var card in round)
                {
                    var cardViewModel = new CardServiceViewModel();

                    cardViewModel.CardRank = card.Card.CardRank.ToString();
                    cardViewModel.CardSuit = card.Card.CardSuit.ToString();

                    userModel.Cards.Add(cardViewModel);
                }
                userModelList.Add(userModel);
            }

            return userModelList;
        }

    }
}
