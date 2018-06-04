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
            ICardRepository<Card> cardRepository = new CardRepository<Card>();
            IGameRepository<Game> gameRepository = new GameRepository<Game>();
            IPlayerRepository<Player> playerRepository = new PlayerRepository<Player>();
            IRoundRepository<Round> roundRepository = new RoundRepository<Round>();

            var gameServiceViewModelFromClient = new GameServiceViewModel { PlayerName = "Sam", BotQuantity = 5 };

            RoundService roundService = new RoundService(roundRepository, cardRepository, gameRepository);

            var gameCreator = new GameService(gameRepository, playerRepository, roundRepository, cardRepository);

            var listOfPlayers = gameCreator.CreateGame(gameServiceViewModelFromClient);

            var player = playerRepository.GetLast();

            Console.WriteLine("First Round");
            foreach (var item in listOfPlayers)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine("=============");
            }

            var newlistOfPlayers = roundService.MapTheModel(listOfPlayers);

            bool ask1;

            do
            {
                Console.WriteLine("=======Enter :");
                string ask = Console.ReadLine();
                bool.TryParse(ask, out ask1);
                Console.WriteLine("==============");
                newlistOfPlayers[0].IsTakeCard = ask1;
                var userModel = roundService.GiveCardToPlayer(newlistOfPlayers);
                Console.WriteLine("n Round");
                Console.WriteLine("=============");
                Console.WriteLine("=============");
                foreach (var item in userModel)
                {
                    Console.WriteLine(item.ToString());
                    Console.WriteLine("=============");
                }
            }
            while (ask1);
        }
    }
}
