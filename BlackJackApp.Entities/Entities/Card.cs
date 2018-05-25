using BlackJackApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Entities
{
    public class Card
    {
        public int Id { get; set; }

        public CardRank CardRank { get; set; }

        public CardSuit CardSuit { get; set; }

        //public Card()
        //{
        //    Array rankValues = Enum.GetValues(typeof(CardRank));
        //    _cardRank = (CardRank)rankValues.GetValue(rnd.Next(rankValues.Length));

        //    Array ColorValues = Enum.GetValues(typeof(CardColor));
        //    _cardColor = (CardColor)rankValues.GetValue(rnd.Next(rankValues.Length));
        //}

       
    }
}
