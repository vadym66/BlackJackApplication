﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlackJackApp.DAL.EF;
using BlackJackApp.DataAccess.Interface;
using BlackJackApp.Entities;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Entities.Enums;
using BlackJackApp.Services;

namespace BlackJackApp.Services
{
    public class CardRepository : ICardRepository
    {
        private BlackJackDbContext _db;


        public CardRepository()
        {
            _db = new BlackJackDbContext();
        }

        public void Create(Card card)
        {
            _db.Cards.Add(card);
        }

        public IEnumerable<Card> GetAll()
        {
            return _db.Cards;
        }

        public Card GetCard(int id)
        {
            return _db.Cards.Find(id);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}