namespace BlackJackApp.ViewModels
{
    public class RoundServiceViewModel
    {
        public int Round { get; set; }

        public string Name { get; set; }

        public CardServiceViewModel CurrentCard { get; set; }

        public RoundServiceViewModel()
        {
            CurrentCard = new CardServiceViewModel();
        }
    }
}
