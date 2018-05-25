using BlackJackApp.Entities;
using BlackJackApp.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJackApp.DAL.EF
{
    public class BlackJackDbContext : DbContext
    {
        public BlackJackDbContext() : base("name=BlackJackDBContext")
        {

        }

        public DbSet<Player> Players { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<Round> Rounds { get; set; }
    }
}
