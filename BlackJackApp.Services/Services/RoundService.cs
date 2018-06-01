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

        public List<NewUserViewModel> GiveCardToPlayer(List<NewUserViewModel> listOfUserViewModels)
        {
            NewUserViewModel _dealer;
            List<NewUserViewModel> _listOfNewUserViewModels = new List<NewUserViewModel>();
            int _dealerIndex = listOfUserViewModels.Count - 1;

            if (listOfUserViewModels[0].IsTakeCard == true)
            {
                _listOfNewUserViewModels = CreateRound(listOfUserViewModels);

                _dealer = _listOfNewUserViewModels[_dealerIndex];
                _dealer = CheckDealer(_dealer);
                _listOfNewUserViewModels[_dealerIndex] = _dealer;

                _listOfNewUserViewModels = CheckForMoreThanTwentyOne(_listOfNewUserViewModels);

                if (CheckForWinner(_listOfNewUserViewModels))
                {
                    return _listOfNewUserViewModels;
                }

                return _listOfNewUserViewModels;

            }

            _dealer = listOfUserViewModels[_dealerIndex];
            _dealer = CheckDealer(_dealer);
            listOfUserViewModels[_dealerIndex] = _dealer;
            listOfUserViewModels = CheckForMoreThanTwentyOne(listOfUserViewModels);

            listOfUserViewModels = FinalCountPoints(listOfUserViewModels);

            return listOfUserViewModels;

            //if (listOfUserViewModels[0].IsTakeCard == false)
            //{
            //    _dealer = listOfUserViewModels[_dealerIndex];
            //    _dealer = CheckDealer(_dealer);
            //    listOfUserViewModels[_dealerIndex] = _dealer;

            //    listOfUserViewModels = FinalCountPoints(listOfUserViewModels);
            //}
        }

        public List<NewUserViewModel> CreateRound(List<NewUserViewModel> listOfMappedModels)
        {
            for (int i = 0; i < listOfMappedModels.Count - 1; i++)
            {
                _card = new Card();
                _card = _cardRepository.GetRandom();

                _round = new Round();
                _round.PlayerId = listOfMappedModels[i].PlayerId;
                _round.CardId = _card.Id;

                _listOfNewUserViewModels[i].CurrentCard.CardRank = _card.CardRank.ToString();
                _listOfNewUserViewModels[i].CurrentCard.CardSuit = _card.CardSuit.ToString();
                _listOfNewUserViewModels[i].SumOfCards += _card.Weight;

                _roundRepository.Add(_round, listOfMappedModels[i].GameId);


            }

            return listOfMappedModels;
        }

        private NewUserViewModel CreateRoundForDealer(NewUserViewModel dealer)
        {

            _card = new Card();
            _card = _cardRepository.GetRandom();

            _round = new Round();
            _round.PlayerId = dealer.PlayerId;
            _round.CardId = _card.Id;

            dealer.CurrentCard.CardRank = _card.CardRank.ToString();
            dealer.CurrentCard.CardSuit = _card.CardSuit.ToString();
            dealer.SumOfCards += _card.Weight;

            //Console.WriteLine(dealer.ToString());

            _roundRepository.Add(_round, dealer.GameId);

            return dealer;
        }

        public List<NewUserViewModel> MapTheModel(List<UserViewModel> userViewModels)
        {
            for (int i = 0; i < userViewModels.Count; i++)
            {
                _newUserViewModel = new NewUserViewModel();

                _newUserViewModel.GameId = userViewModels[i].GameId;
                _newUserViewModel.PlayerId = userViewModels[i].PlayerId;
                _newUserViewModel.Name = userViewModels[i].Name;
                _newUserViewModel.IsWinner = userViewModels[i].IsWinner;
                _newUserViewModel.SumOfCards = userViewModels[i].SumOfCards;

                _listOfNewUserViewModels.Add(_newUserViewModel);
            }

            return _listOfNewUserViewModels;
        }

        private NewUserViewModel CheckDealer(NewUserViewModel dealer)
        {
            if (dealer.SumOfCards < 17)
            {
                do
                {
                    dealer = CreateRoundForDealer(dealer);
                }
                while (dealer.SumOfCards < 17);
            }

            return dealer;
        }

        private bool CheckForWinner(List<NewUserViewModel> listofuserViewModels)
        {
            for (int i = 0; i < listofuserViewModels.Count; i++)
            {
                if (listofuserViewModels[i].SumOfCards == 21)
                {
                    listofuserViewModels[i].IsWinner = true;
                    return true;
                }
            }
            return false;
        }

        public List<NewUserViewModel> FinalCountPoints(List<NewUserViewModel> listOfNewUserViewModels)
        {
            var player = listOfNewUserViewModels[0];
            var dealer = listOfNewUserViewModels[listOfNewUserViewModels.Count - 1];

            if (player.SumOfCards == dealer.SumOfCards)
            {
                Console.WriteLine("draWWW");
            }

            if (player.SumOfCards > dealer.SumOfCards &&
                player.SumOfCards <= 21 &&
                dealer.SumOfCards <= 21)
            {
                player.IsWinner = true;
            }
            dealer.IsWinner = true;

            return listOfNewUserViewModels;
        }

        private List<NewUserViewModel> CheckForMoreThanTwentyOne(List<NewUserViewModel> players)
        {
            if (players[0].SumOfCards > 21)
            {
                return FinalCountPoints(players);
            }

            return players;
        }
    }
}
