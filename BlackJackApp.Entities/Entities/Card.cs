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

        public CardRank CardRank { get; set; }

        public CardSuit CardSuit { get; set; }

        public int Weight { get; set; }

        public override string ToString()
        {
            return $"{Id} :: {CardRank} - {Weight} : {CardSuit}";
        }
    }
}
