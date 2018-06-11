using BlackJackApp.ViewModels;
using System.Collections.Generic;

namespace BlackJackApp.Services
{
    public class RoundViewModel
    {
        public bool isResultComplete { get; set; }

        public List<UserViewModel> Users { get; set; }

        public RoundViewModel()
        {
            Users = new List<UserViewModel>();
        }
    }
}