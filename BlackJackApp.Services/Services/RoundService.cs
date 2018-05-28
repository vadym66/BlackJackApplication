using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services
{
    public class RoundService
    {
        private IRoundRepository _db;
        private List<Player> _playerlist;
        private CardService _cardService;
        private PlayerService _playerService;
        private Round _round;

        public RoundService(List<Player> playerlist)
        {
            _db = new RoundRepository();
            _playerlist = playerlist;
            _round = new Round();
        }

        public void CreateRound(Player player)
        {

        }
    }
}
