using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services.ServiceInterfaces;
using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using AutoMapper;
using AutoMapper.Configuration;
using System.Threading.Tasks;
using BlackJackApp.Services.Services;

namespace BlackJackApp.Services
{
    public class RoundService : IRoundService
    {
        private IRoundRepository<Round> _roundRepository;
        private ICardRepository<Card> _cardRepository;
        private IGameRepository<Game> _gameRepository;

        public RoundService(IRoundRepository<Round> roundRepository, ICardRepository<Card> cardRepository, IGameRepository<Game> gameRepository)
        {
            _roundRepository = roundRepository;
            _cardRepository = cardRepository;
            _gameRepository = gameRepository;
        }

        public async Task<List<UserViewModel>> CreateRoundLogic(List<UserViewModel> listOfUserViewModels)
        {
            var player = listOfUserViewModels[0];
            var dealer = listOfUserViewModels[listOfUserViewModels.Count - 1];

            if (player.IsTakeCard == true)
            {
                listOfUserViewModels = await CreateRoundForAllPlayers(listOfUserViewModels);
                dealer = await CheckDealer(dealer);
                listOfUserViewModels[listOfUserViewModels.Count - 1] = dealer;
                return CheckForWinner(listOfUserViewModels);
            }

            return CheckForWinner(listOfUserViewModels);
        }


        public async Task<List<UserViewModel>> CreateRoundForAllPlayers(List<UserViewModel> listOfUserViewModels)
        {
            Round round;
            Card card;

            for (int i = 0; i < listOfUserViewModels.Count - 1; i++)
            {
                card = new Card();
                card = await _cardRepository.GetRandom();

                round = new Round();
                round.PlayerId = listOfUserViewModels[i].PlayerId;
                round.CardId = card.Id;
                await _roundRepository.Add(round, listOfUserViewModels[i].GameId);

                listOfUserViewModels[i].CurrentCard.Add(new CardServiceViewModel());
                int lastCard = listOfUserViewModels[i].CurrentCard.Count - 1;

                listOfUserViewModels[i].CurrentCard[lastCard].CardRank = card.CardRank.ToString();
                listOfUserViewModels[i].CurrentCard[lastCard].CardSuit = card.CardSuit.ToString();
                listOfUserViewModels[i].SumOfCards += card.Weight;
            }
            return listOfUserViewModels;
        }

        private async Task<UserViewModel> CreateRoundForDealer(UserViewModel dealer)
        {
            var card = new Card();
            card = await _cardRepository.GetRandom();

            var round = new Round();
            round.PlayerId = dealer.PlayerId;
            round.CardId = card.Id;
            await _roundRepository.Add(round, dealer.GameId);

            dealer.CurrentCard.Add(new CardServiceViewModel());
            int lastCard = dealer.CurrentCard.Count - 1;

            dealer.CurrentCard[lastCard].CardRank = card.CardRank.ToString();
            dealer.CurrentCard[lastCard].CardSuit = card.CardSuit.ToString();
            dealer.SumOfCards += card.Weight;

            return dealer;
        }

        private async Task<UserViewModel> CheckDealer(UserViewModel dealer)
        {
            if (dealer.SumOfCards < 17)
            {
                do
                {
                    dealer = await CreateRoundForDealer(dealer);
                }
                while (dealer.SumOfCards < 17);
            }

            return dealer;
        }

        private List<UserViewModel> CheckForWinner(List<UserViewModel> userViewModels)
        {
            var dealer = userViewModels[userViewModels.Count - 1];
            var player = userViewModels[0];

            if (dealer.SumOfCards == 21)
            {
                dealer.IsWinner = Enums.WinnerFlag.winner;
                return userViewModels;
            }

            if (player.SumOfCards == dealer.SumOfCards)
            {
                player.IsWinner = Enums.WinnerFlag.draw;
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

        public List<UserViewModel> FinalCount(List<UserViewModel> userViewModels)
        {
            userViewModels.Sort(new ComparerForUserView());
            userViewModels[userViewModels.Count - 1].IsWinner = Enums.WinnerFlag.winner;
            return userViewModels;
        }
    }
}