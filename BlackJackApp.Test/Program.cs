using BlackJackApp.Entities.Entities;
using BlackJackApp.Services;
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
            var playerRepository = new PlayerRepository();

            var players = playerRepository.GetSequence(3);

            foreach (var item in players)
            {
                Console.WriteLine(item);
            }
            //var cardRepository = new CardRepository();
            //var roundRepository = new RoundRepository();
            //var gameRepository = new GameRepository();

            //var gameService = new GameService(gameRepository);
            //var playerService = new PlayerService(playerRepository);
            //var roundService = new RoundService(roundRepository, cardRepository, gameRepository);
            //var cardService = new CardService(cardRepository);

            //string name = "Sam";
            //var startGame = gameService.CreateGame(null, name);

            //var human = playerService.CreateHumanPlayer(startGame.PlayerName);
            //var dealer = playerService.CreateDealer();

            //var createRoundforHuman = roundService.CreateRound(human.PlayerId, startGame.GameId);
            //var createRoundforHuman1 = roundService.CreateRound(human.PlayerId, startGame.GameId);
            //Console.WriteLine();

            //var createRoundforDealer = roundService.CreateRound(dealer.PlayerId, startGame.GameId);
            //var createRoundforDealer2 = roundService.CreateRound(dealer.PlayerId, startGame.GameId);

            //var createRoundforHuman3 = roundService.CreateRound(human.PlayerId, startGame.GameId);

            //var createRoundforDealer3 = roundService.CreateRound(dealer.PlayerId, startGame.GameId);


            //string name = "peter";
            //playerRepository.Add(new Player {Name = name });

        }
    }
}
