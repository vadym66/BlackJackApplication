using BlackJackApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Entities.Entities
{
    public class RoundState
    {
        public int Id { get; set; }

        public int CardSum { get; set; }

        public EntityPlayerStatus Status { get; set; }
    }
}
