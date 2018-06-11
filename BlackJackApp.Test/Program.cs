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
            HistoryService history = new HistoryService(gameRepository, roundRepository);

            GameService service = new GameService(gameRepository, playerRepository, roundRepository, cardRepository);

            #region
            //var uiViewModel = new GameServiceViewModel { PlayerName = "Scott", BotQuantity = 2 };
            //var result = await service.StartGame(uiViewModel);

            //foreach (var user in result.Users)
            //{
            //    Console.WriteLine(user.ToString());
            //    foreach (var card in user.Cards)
            //    {
            //        Console.WriteLine($"{ card.CardRank} : { card.CardSuit}");
            //    }
            //    Console.WriteLine("========================");
            //}

            //Console.ReadKey();
            //Console.WriteLine("===================SECOND ROUND===================");
            //Console.WriteLine("===================SECOND ROUND===================");

            //var resultAfteNextRound = await service.StartNextRoundForPlayers(result.Users);

            //foreach (var user in resultAfteNextRound.Users)
            //{
            //    Console.WriteLine(user.ToString());
            //    foreach (var card in user.Cards)
            //    {
            //        Console.WriteLine($"{ card.CardRank} : { card.CardSuit}");
            //    }
            //    Console.WriteLine("========================");
            //}
            //Console.ReadKey();
            #endregion

            var query = await history.GetAllRoundsFromParticularGame(77);

            var roundhistory = history.CreateUserHistoryVM(query);

            foreach (var round in roundhistory)
            {
                Console.WriteLine(round.UserName);
                foreach (var card in round.Cards)
                {
                    Console.WriteLine($"{card.CardRank} : {card.CardSuit}");
                }
            }

        }
    }
}
