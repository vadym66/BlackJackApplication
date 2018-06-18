using BlackJackApp.Entities.Entities;
using BlackJackApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services.ServiceInterfaces
{
    public interface IHistoryService
    {
        Task<IEnumerable<Round>> GetAllRoundsFromParticularGame(int gameId);
        List<UserHistoryViewModel> CreateUserHistoryVM(IEnumerable<Round> rounds);
        Task<IEnumerable<GameViewModel>> GetLastTenGames();
    }
}
