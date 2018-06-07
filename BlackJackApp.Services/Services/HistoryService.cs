using BlackJackApp.DAL.Repositories;
using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services.Services
{
    public class HistoryService
    {
        private HistoryRepository _historyRepository = new HistoryRepository();

        public HistoryService()
        {
            
        }
                
        public async Task<IEnumerable<Player>> GetGames()
        {
            return await _historyRepository.GetGame();
        }

        public async Task<GameDetailsViewModel> GetGameDetails(int gameId)
        {
            var rounds = await _historyRepository.GetRound(gameId);

            var query = from r in rounds
                        group r by r.PlayerId into membersByGroupCode
                        select membersByGroupCode;
        }


    }
}
