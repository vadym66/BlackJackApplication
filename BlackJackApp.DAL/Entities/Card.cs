using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApplication.Enums;

namespace BlackJackApplication.Entities
{
    public class Card
    {
        private CardColor _cardColor;
        private CardRank _cardRank;

        static Random rnd = new Random();

        //public Card()
        //{
        //    Array rankValues = Enum.GetValues(typeof(CardRank));
        //    _cardRank = (CardRank)rankValues.GetValue(rnd.Next(rankValues.Length));

        //    Array ColorValues = Enum.GetValues(typeof(CardColor));
        //    _cardColor = (CardColor)rankValues.GetValue(rnd.Next(rankValues.Length));
        //}

       
    }
}
