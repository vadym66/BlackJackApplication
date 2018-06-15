using BlackJackApp.DAL.Interfaces;
using BlackJackApp.DAL.Repositories;
using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services;
using BlackJackApp.Services.ServiceInterfaces;
using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlackJackApp.Controllers.Controllers
{
    public class GameController : Controller
    {
        ICardRepository<Card> cardRepository = new CardRepository<Card>();
        IGameRepository<Game> gameRepository = new GameRepository<Game>();
        IPlayerRepository<Player> playerRepository = new PlayerRepository<Player>();
        IRoundRepository<Round> roundRepository = new RoundRepository<Round>();
        IPlayersGameRepository<Player> playerGamesRepository = new PlayersGameRepository();
        private IGameService<GameService> _gameService;

        public GameController()
        {
            _gameService = new GameService(gameRepository, playerRepository, roundRepository, cardRepository, playerGamesRepository);
        }

        [HttpGet]
        public async Task<ActionResult> StartGame()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> CurrentGame(GameServiceViewModel viewModel)
        {
            var result = await _gameService.StartGame(viewModel);
            //if (result.isResultComplete)
            //{
            //    return View("GameFinnished", result);
            //}

            return View(result);
        }

        [HttpPost]
        public  async Task<ActionResult> StartNextRoundForPlayers(RoundViewModel rounds)
        {
            var result = await _gameService.StartNextRoundForPlayers(rounds.Users);
            ModelState.Clear();
            //if (result.isResultComplete)
            //{
            //    return View("GameFinnished", result);
            //}

            return PartialView(result);
        }

        [HttpPost]
        public async Task<ActionResult> StartNextRoundForDealer(RoundViewModel rounds)
        {
            var result = await _gameService.StartNextRoundForDealer(rounds.Users);
            ModelState.Clear();
            
            return View("GameFinnished", result);
        }
    }
}
