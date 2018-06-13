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
        private IGameService<GameService> _gameService;

        public GameController()
        {
            _gameService = new GameService(gameRepository, playerRepository, roundRepository, cardRepository);
        }

        [HttpGet]
        public async Task<ActionResult> StartGame()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> StartGame(GameServiceViewModel viewModel)
        {
            var result = await _gameService.StartGame(viewModel);

            return View("CurrentGame", result);
        }

        [HttpPost]
        public  async Task<ActionResult> ContinueGame(RoundViewModel rounds)
        {
            var result = await _gameService.StartNextRoundForPlayers(rounds.Users);
            ModelState.Clear();
            return View("CurrentGame", result);
        }

        [HttpPost]
        public async Task<ActionResult> DealerTakesCard(RoundViewModel rounds)
        {
            var result = await _gameService.StartNextRoundForDealer(rounds.Users);
            ModelState.Clear(); ////// !!!!!!!!!
            return View("CurrentGame", result);
        }
    }
}
