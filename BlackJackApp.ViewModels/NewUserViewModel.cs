using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.ViewModels
{
    public class NewUserViewModel
    {
        public int GameId { get; set; }

        public int PlayerId { get; set; }

        public int CardId { get; set; }

        public int RoundId { get; set; }

        public string Name { get; set; }

        public CardServiceViewModel CurrentCard { get; set; }

        public int SumOfCards { get; set; }

        public bool IsWinner { get; set; }

        public NewUserViewModel()
        {
            CurrentCard = new CardServiceViewModel();
        }

        public override string ToString()
        {
            return $"Name:{Name} gameId:{GameId} playerId:{PlayerId} roundId:{RoundId} IsWinner:{IsWinner}  : {CurrentCard.CardRank}/{CurrentCard.CardSuit}  /{SumOfCards}";
        }
    }
}
