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

        public virtual EntityPlayerRole PlayerRole { get; set; }
    }
}
