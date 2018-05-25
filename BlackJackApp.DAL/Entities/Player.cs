using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApplication.Entities
{
    public class Player
    {
        public int PlayerId { get; set; }

        public string PlayerName { get; set; }

        public Card CurrentCard{ get; set; }

        public int CurrrentRound { get; set; }

        public History MyProperty { get; set; }
    }
}
