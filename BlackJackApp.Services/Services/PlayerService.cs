using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services
{
    public class PlayerService
    {
        private IPlayerRepository<Player> _db;
        private List<Player> _playersList;

        public PlayerService()
        {
            _db = new PlayerRepository();
            _playersList = new List<Player>();
        }

        public void CreatePlayer()
        {
            Player player = new Player();

            player.Name = Console.ReadLine();
            _playersList.Add(player);
            _db.Create(player);
            _db.Save();
        }

        public void CreateBot(int numberOfBots)
        {
            for (int i = 0; i < numberOfBots; i++)
            {
                _playersList.Add(new Player());
                _db.Create(_playersList[i]);
                _db.Save();
            }
        }

        public void CreateDelear()
        {
            Player player = new Player();

            _playersList.Add(player);
            _db.Create(player);
            _db.Save();
        }

        public List<Player> GetAll()
        {
            return _playersList;
        }
    }
}
