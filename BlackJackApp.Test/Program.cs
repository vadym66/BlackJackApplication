using BlackJackApp.DAL.Repositories;
using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services;
using BlackJackApp.Services.Services;
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

            GameService service = new GameService(gameRepository, playerRepository, roundRepository, cardRepository);

            var uiViewModel = new GameServiceViewModel { PlayerName = "Scott", BotQuantity = 2 };
            var result = await service.StartGame(uiViewModel);


            foreach (var user in result.Users)
            {
                Console.WriteLine(user.UserName);
                foreach (var card in user.Cards)
                {
                    Console.WriteLine($"{ card.CardRank} : { card.CardSuit}");
                }
                Console.WriteLine("========================");
            }
            Console.ReadKey();
        }
    }
}
