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
            GameRepository gameRepository = new GameRepository();
            PlayerRepository repo = new PlayerRepository();
            //repo.DeleteAll();

            //repo.Add(new Player { Name = "Peter" });
            //repo.Add(new Player { Name = "Peter" });
            //repo.Add(new Player { Name = "Peter" });
            //repo.Add(new Player { Name = "Peter" });

            //gameRepository.Add(new Game());

            //var game = gameRepository.GetLast();
            //Console.WriteLine(game.Id.ToString());

            Game game = new Game();
            gameRepository.Add(game);

            Console.WriteLine(game.Id.ToString());


            //gameRepository.Add(new Game());
            //game = gameRepository.GetLast();
            ////Console.WriteLine(game.Id.ToString());
            //gameRepository.DeleteAll();


        }
    }
}
