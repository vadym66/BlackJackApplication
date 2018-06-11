using BlackJackApp.Services.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BlackJackApp.Controllers.Controllers
{
    public class HistoryController : Controller
    {
        private IHistoryService _historyService;

        public HistoryController(IHistoryService historyService)
        {
            _historyService = historyService;
        }
        // GET: History
        public ActionResult Index()
        {
            return View();
        }
    }
}