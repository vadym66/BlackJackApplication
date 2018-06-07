using BlackJackApp.DAL.Repositories;
using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services;
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
            HistoryRepository historyRepository = new HistoryRepository();

            var games = await historyRepository.GetGame();
            var rounds = await historyRepository.GetRound(3);
            

            var query = from r in rounds
                        group r by r.PlayerId into membersByGroupCode
                        select membersByGroupCode;

            
        }
    }
}
