using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.Entities;

namespace BlackJackApp.DataAccess.Interface

{ 
    public interface ICardRepository
    {
        void Create();
        Card GetCard(int id);
        void Save();
    }
}
