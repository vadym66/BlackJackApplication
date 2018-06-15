//using BlackJackApp.DAL.Interfaces;
//using BlackJackApp.DAL.Repositories;
//using BlackJackApp.DataAccess.Interface;
//using BlackJackApp.Entities.Entities;
//using BlackJackApp.Services;
//using BlackJackApp.Services.ServiceInterfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;
//using System.Web.Http;

//namespace BlackJackApp.Controllers.Controllers
//{
//    public class GameApiController : ApiController
//    {
//        ICardRepository<Card> cardRepository = new CardRepository<Card>();
//        IGameRepository<Game> gameRepository = new GameRepository<Game>();
//        IPlayerRepository<Player> playerRepository = new PlayerRepository<Player>();
//        IRoundRepository<Round> roundRepository = new RoundRepository<Round>();
//        IPlayersGameRepository<Player> playerGamesRepository = new PlayersGameRepository();
//        private IGameService<GameService> _gameService;

//        public GameApiController()
//        {
//            _gameService = new GameService(gameRepository, playerRepository, roundRepository, cardRepository, playerGamesRepository);
//        }

//        [HttpPost]
//        public async Task<IHttpActionResult> StartNextRoundForPlayers(RoundViewModel rounds)
//        {
//            var result = await _gameService.StartNextRoundForPlayers(rounds.Users);
//            ModelState.Clear();
//            //if (result.isResultComplete)
//            //{
//            //    return View("GameFinnished", result);
//            //}

//            return Ok(result);
//        }
//    }
//}
