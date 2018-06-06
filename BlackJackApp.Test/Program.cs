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
        static async Task Main(string[] args)
        {
            ICardRepository<Card> cardRepository = new CardRepository<Card>();
            IGameRepository<Game> gameRepository = new GameRepository<Game>();
            IPlayerRepository<Player> playerRepository = new PlayerRepository<Player>();
            IRoundRepository<Round> roundRepository = new RoundRepository<Round>();

            GameService gameService = new GameService(gameRepository, playerRepository, roundRepository, cardRepository);
            RoundService roundService = new RoundService(roundRepository, cardRepository, gameRepository, playerRepository);

            var gameModel = await gameService.CreateGame(new GameServiceViewModel { PlayerName = "Scott", BotQuantity = 1 });

            var usermodels = await roundService.CreateFirstRound(gameModel);
            Console.WriteLine("==========SecondRound==============");
            usermodels = await roundService.CreateNextRound(usermodels);

           // usermodels = await roundService.TotalCount(usermodels);

            foreach (var item in usermodels)
            {
                Console.WriteLine(item.ToString());
                foreach (var card in item.CurrentCard)
                {
                    Console.WriteLine(card.ToString());
                }
            }


        }
    }
}
