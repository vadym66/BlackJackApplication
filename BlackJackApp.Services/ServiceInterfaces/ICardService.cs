﻿using BlackJackApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services.ServiceInterfaces
{
    public interface ICardService
    {
        Card GetCard();
    }
}