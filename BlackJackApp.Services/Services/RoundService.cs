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
    public class RoundService : IRoundService
    {
        private IRoundRepository _roundRepository;
        private ICardRepository _cardRepository;
        private IGameRepository _gameRepository;
        private Round _round;

        public RoundService(IRoundRepository roundRepository, ICardRepository cardRepository, IGameRepository gameRepository)
        {
            _roundRepository = roundRepository;
            _cardRepository = cardRepository;
            _gameRepository = gameRepository;
        }

        public RoundServiceViewModel CreateRound(int userId, int gameId)
        {
            _round = new Round();

            var card = _cardRepository.GetRandom();

            _round.PlayerId = userId;
            _round.CardId = card.Id;

            _roundRepository.Add(_round, gameId);

            var roundServiceViewModel = new RoundServiceViewModel();
            roundServiceViewModel.Round = _round.Id;
            roundServiceViewModel.Card.CardRank = card.CardRank.ToString();
            roundServiceViewModel.Card.CardSuit = card.CardSuit.ToString();

            return roundServiceViewModel;
        }
    }
}
