using BlackJackApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Entities.Entities
{
    public class Player
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public EntityPlayerRole PlayerRole { get; set; }

        public EntityGameStatus GameStatus { get; set; }
    }
}
