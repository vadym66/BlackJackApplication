namespace BlackJackApp.Entities.Entities
{
    public class Round
    {
        public int Id { get; set; }

        public int PlayerId { get; set; }

        public int CardId { get; set; }

        public override string ToString()
        {
            return $"RoundId: {Id}  PlayerId: {PlayerId}  CardId: {CardId}";
        }
    }
}