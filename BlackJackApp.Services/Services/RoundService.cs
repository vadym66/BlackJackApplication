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
        private Card _card;
        private NewUserViewModel _newUserViewModel;

        private List<NewUserViewModel> _listOfNewUserViewModels;

        public RoundService(IRoundRepository roundRepository, ICardRepository cardRepository, IGameRepository gameRepository)
        {
            _roundRepository = roundRepository;
            _cardRepository = cardRepository;
            _gameRepository = gameRepository;
            _listOfNewUserViewModels = new List<NewUserViewModel>();
        }

        public List<NewUserViewModel> CreateRound(List<UserViewModel> userViewModels)
        {
            for (int i = 0; i < userViewModels.Count; i++)
            {
                _card = new Card();
                _card = _cardRepository.GetRandom();

                _round = new Round();
                _round.PlayerId = userViewModels[i].PlayerId;
                _round.CardId = _card.Id;
                _roundRepository.Add(_round, userViewModels[i].GameId);

                _newUserViewModel = new NewUserViewModel();

                _listOfNewUserViewModels[i].CurrentCard

                _listOfNewUserViewModels.Add(userViewModels[i]);
            }

            return _listOfUserViewModels;
        }


    }
}
