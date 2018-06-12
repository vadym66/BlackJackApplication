using System.Collections.Generic;

namespace BlackJackApp.ViewModels
{
    public class GameViewModel
    {
        public int GameId { get; set; }

        public List<string> WinnnerNames { get; set; }
    }
}