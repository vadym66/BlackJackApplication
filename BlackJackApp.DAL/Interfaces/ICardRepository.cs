using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.Entities;
using BlackJackApp.Entities.Entities;

namespace BlackJackApp.DataAccess.Interface

{ 
    public interface ICardRepository
    {
        IEnumerable<Card> GetAll();
        void Create(Card card);
        Card GetCard(int id);
        void Save();
    }
}
