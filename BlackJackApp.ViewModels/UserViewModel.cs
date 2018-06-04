using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.ViewModels
{
    public class UserViewModel 
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public int RoundId { get; set; }

        public string Name { get; set; }

        public int SumOfCards { get; set; }

        public List<CardServiceViewModel> CurrentCard{ get; set; }

        public bool IsWinner { get; set; }

        public bool IsTakeCard { get; set; }

        public UserViewModel()
        {
            CurrentCard1 = new CardServiceViewModel();
            CurrentCard2 = new CardServiceViewModel();
        }

        public override string ToString()
        {
            return $"Name:{Name} gameId:{GameId} playerId:{PlayerId} roundId:{RoundId} IsWinner:{IsWinner}  : {CurrentCard1.CardRank}/{CurrentCard1.CardSuit}   {CurrentCard2.CardRank}/{CurrentCard2.CardSuit}  /{SumOfCards}";
        }
    }
}
