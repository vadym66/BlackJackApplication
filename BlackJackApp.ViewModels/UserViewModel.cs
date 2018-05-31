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

        public int FirstCardId { get; set; }

        public int SecondCardId { get; set; }

        public string Name { get; set; }

        public CardServiceViewModel CurrentCard1 { get; set; }

        public CardServiceViewModel CurrentCard2 { get; set; }

        public bool IsWinner { get; set; }

        public UserViewModel()
        {
            CurrentCard1 = new CardServiceViewModel();
            CurrentCard2 = new CardServiceViewModel();
        }

        public override string ToString()
        {
            return $"Name:{Name} gameId:{GameId} playerId:{PlayerId} IsWinner:{IsWinner}  : {CurrentCard1.CardRank}/{CurrentCard1.CardSuit}   {CurrentCard2.CardRank}/{CurrentCard2.CardSuit}  /{CurrentCard1.CardWeight + CurrentCard2.CardWeight}";
        }
    }
}
