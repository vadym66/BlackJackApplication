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
        IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
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