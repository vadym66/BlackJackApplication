namespace BlackJackApp.ViewModels
{
    public class CardServiceViewModel
    {
        public int CardId { get; set; }

        public string CardRank { get; set; }

        public string CardSuit { get; set; }

        public int CardWeight { get; set; }

        public override string ToString()
        {
            return $"CardId: {CardId} {CardRank}/{CardSuit}";
        }
    }
}