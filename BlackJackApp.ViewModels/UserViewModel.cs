using BlackJackApp.Services.Enums;
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

        public int PlayerId { get; set; }

        public int CardId { get; set; }

        public int RoundId { get; set; }

        public string Name { get; set; }

        public int SumOfCards { get; set; }

        public List<CardServiceViewModel> CurrentCard { get; set; }

        public WinnerFlag IsWinner { get; set; } = WinnerFlag.notWinner;

        public bool IsTakeCard { get; set; }

        public UserViewModel()
        {
            CurrentCard = new List<CardServiceViewModel>();
        }

        public override string ToString()
        {
            return $"Name: {Name}  PlayeriD: {PlayerId}  GameId{GameId}  Sum of cards: {SumOfCards}  Winner: {IsWinner}";
        }
    }
}
