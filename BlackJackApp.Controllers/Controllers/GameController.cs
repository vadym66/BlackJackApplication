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
        // GET: Game
        public ActionResult Index()
        {
            return View();
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
            return View("CurrentGame", result);
        }

        // GET: Game/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Game/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Game/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Game/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Game/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Game/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
