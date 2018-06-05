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
            RoundService roundService = new RoundService(roundRepository, cardRepository, gameRepository);

            var usermodels = await gameService.CreateGame(new GameServiceViewModel { PlayerName = "Scott", BotQuantity = 0 });

            foreach (var item in usermodels)
            {
                Console.WriteLine(item.ToString());
            }

            
        }
    }
}
