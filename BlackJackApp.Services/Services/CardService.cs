using BlackJackApp.DAL.EF;
using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Services
{
    public class CardService
    {
        private ICardRepository _db;

        public CardService()
        {
            _db = new CardRepository();
        }

        public int CreateCard()
        {
            Card card = new Card();
            Random rnd = new Random();

            Array cardRank = Enum.GetValues(typeof(CardRank));
            card.CardRank = (CardRank)cardRank.GetValue(rnd.Next(cardRank.Length));

            Array cardSuit = Enum.GetValues(typeof(CardSuit));
            card.CardSuit = (CardSuit)cardSuit.GetValue(rnd.Next(cardSuit.Length));

            _db.Create(card);
            _db.Save();

            return card.Id; 
        }                
    }
}
