using BlackJackApp.DAL.EF;
using BlackJackApp.Entities.Entities;
using BlackJackApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.Console.Test
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var db = new BlackJackDbContext())
            {
                var cr = new CardRepository();

                cr.Create();
                cr.Save();
                cr.Create();
                cr.Save();
                cr.Create();
                cr.Save();

                foreach (var item in db.Cards)
                {
                    System.Console.WriteLine($"({item.CardRank} : {item.CardSuit}");
                }
            }
        }
    }
}

