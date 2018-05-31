using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services.ServiceInterfaces;
using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Configuration; 

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

                _newUserViewModel.GameId = userViewModels[i].GameId;
                _newUserViewModel.PlayerId = userViewModels[i].PlayerId;
                _newUserViewModel.Name = userViewModels[i].Name;
                _newUserViewModel.IsWinner = userViewModels[i].IsWinner;
                _newUserViewModel.SumOfCards = userViewModels[i].SumOfCards + _card.Weight;
                _newUserViewModel.CurrentCard.CardRank = _card.CardRank.ToString();
                _newUserViewModel.CurrentCard.CardSuit = _card.CardSuit.ToString();
                
                _listOfNewUserViewModels.Add(_newUserViewModel);
            }

            return _listOfNewUserViewModels;
        }
    }
}
