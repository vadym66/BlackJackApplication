using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services;
using BlackJackApp.Services.ServiceInterfaces;
using BlackJackApp.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace BlackJackApp.Controllers.Controllers
{
    public class HistoryController : Controller
    {
        ICardRepository<Card> cardRepository = new CardRepository<Card>();
        IGameRepository<Game> gameRepository = new GameRepository<Game>();
        IPlayerRepository<Player> playerRepository = new PlayerRepository<Player>();
        IRoundRepository<Round> roundRepository = new RoundRepository<Round>();

        HistoryService _historyService;

        public HistoryController()
        {
            _historyService = new HistoryService(gameRepository, roundRepository);
        }

        public async Task<ActionResult> ShowGames()
        {
            var games = await _historyService.GetLastTenGames();

            return View(games);
        }

        
        public async Task<ActionResult> Details(int id)
        {
            var query = await _historyService.GetAllRoundsFromParticularGame(id);
            var result = _historyService.CreateUserHistoryVM(query);

            return View(result);
        }
    }
}