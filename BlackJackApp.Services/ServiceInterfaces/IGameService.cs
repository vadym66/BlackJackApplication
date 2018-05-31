﻿using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services.ServiceInterfaces
{
    public interface IGameService
    {
        List<UserViewModel> CreateGame(GameServiceViewModel gameServiceViewModel);

    }
}
