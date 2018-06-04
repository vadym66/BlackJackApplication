using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.Entities;
using BlackJackApp.Entities.Entities;

namespace BlackJackApp.DataAccess.Interface

{ 
    public interface ICardRepository<T> where T : class
    {
        public async Task<Card> GetRandom();
    }
}
