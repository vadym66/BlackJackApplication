using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services.ServiceInterfaces
{
    public interface IRoundService
    {
        RoundServiceViewModel CreateRound(int userId, int gameId);
    }
}
