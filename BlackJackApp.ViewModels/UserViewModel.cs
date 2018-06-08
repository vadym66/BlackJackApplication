﻿using BlackJackApp.Services.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BlackJackApp.ViewModels
{
    public class UserViewModel 
    {
        public string UserName { get; set; }

        public List<CardServiceViewModel> Cards { get; set; }

        public UserViewModel()
        {
            Cards = new List<CardServiceViewModel>();
        }
    }
}
