using BlackJackApp.DAL.EF;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
                GameService gameService = new GameService();
                gameService.CreateGame(1);
        }
    }
}

