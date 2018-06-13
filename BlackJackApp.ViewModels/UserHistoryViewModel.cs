using System.Collections.Generic;

namespace BlackJackApp.ViewModels
{
    public class UserHistoryViewModel
    {
        public string UserName { get; set; }

        public List<string> WinnerNames { get; set; }

        public List<CardServiceViewModel> Cards { get; set; }

        public UserHistoryViewModel()
        {
            Cards = new List<CardServiceViewModel>();
        }
    }
}