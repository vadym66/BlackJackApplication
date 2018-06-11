using BlackJackApp.Services.Enums;
using BlackJackApp.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlackJackApp.ViewModels
{
    public class UserViewModel 
    {
        public int GameId { get; set; }

        public int RoundId { get; set; }

        public int PlayerId { get; set; }

        public int CardSum { get; set; }

        public string UserName { get; set; }

        public WinnerFlag winnerFlag { get; set; } = WinnerFlag.isNotWinner;

        public PlayerRole PlayerRole { get; set; }

        public List<CardServiceViewModel> Cards { get; set; }

        public UserViewModel()
        {
            Cards = new List<CardServiceViewModel>();
        }

        public override string ToString()
        {
            return $"Name: {UserName}  CardSum{CardSum}";
        }
    }
}
