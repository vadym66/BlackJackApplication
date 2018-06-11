using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BlackJackApp.Entities.Entities
{
    public class Round
    {
        public int Id { get; set; }
        
        public int PlayerId { get; set; }

        public int CardId { get; set; }

        public int GameId { get; set; }

        public virtual Card Card { get; set; }

        public virtual Player Player { get; set; }

        public override string ToString()

       
        {
            return $"RoundId: {Id}  PlayerId: {PlayerId}  CardId: {CardId} ";
        }
    }
}