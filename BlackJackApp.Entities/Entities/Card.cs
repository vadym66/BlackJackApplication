using BlackJackApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Entities.Entities
{
    public class Card
    {
        public int Id { get; set; }

        public CardRank Rank { get; set; }

        public CardSuit Suit { get; set; }

        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{Rank} : {Suit}  //id: {Id}";
        }
    }
}
