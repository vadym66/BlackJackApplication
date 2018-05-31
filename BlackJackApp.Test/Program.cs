using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services;
using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Test
{
    class Program
    {   
        static void Main(string[] args)
        {
            ICardRepository cardRepository = new CardRepository();
            IGameRepository gameRepository = new GameRepository();
            IPlayerRepository playerRepository = new PlayerRepository();
            IRoundRepository roundRepository = new RoundRepository();

            var gameServiceViewModelFromClient = new GameServiceViewModel { PlayerName = "Sam", BotQuantity = 3 };

            RoundService roundService = new RoundService(roundRepository, cardRepository, gameRepository);

            var gameCreator = new GameService(gameRepository, playerRepository, roundRepository, cardRepository);

            var listOfPlayers = gameCreator.CreateGame(gameServiceViewModelFromClient);

            Console.WriteLine("First Round");
            foreach (var item in listOfPlayers)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine("=============");
            }

            var userModel = roundService.CreateRound(listOfPlayers);

            Console.WriteLine("Second Round");
            string more = Console.ReadLine();
            if (more == "more")
            {
                foreach (var item in userModel)
                {
                    Console.WriteLine(item.ToString());
                    Console.WriteLine("=============");
                }
            }
        }
    }
}
