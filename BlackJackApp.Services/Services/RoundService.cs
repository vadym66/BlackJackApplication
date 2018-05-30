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

        public RoundService(IRoundRepository roundRepository, ICardRepository cardRepository)
        {
            _roundRepository = roundRepository;
            _cardRepository = cardRepository;
        }

        public RoundServiceViewModel CreateFirstRound(int userId, int gameId)
        {
            var round = new Round();
            var card = _cardRepository.GetRandom();
            

            round.CardId = card.Id;
            round.PlayerId = userId;

            _roundRepository.Add(round, gameId);

            var roundServiceViewModel = new RoundServiceViewModel();


        }

        public RoundServiceViewModel CreateRound(int userId, int gameId)
        {
            throw new Exception();
        }
    }
}
