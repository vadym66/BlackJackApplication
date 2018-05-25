using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Entities.Entities
{
    public class Game
    {
        public int Id { get; set; }

        public List<Round> Rounds { get; set; }
    }
}
