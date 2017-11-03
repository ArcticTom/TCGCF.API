namespace TCGCF.API.Models
{
    public class CardsInDeckDTO
    {
        public int Id { get; set; }
        public CardDTO Card { get; set; } = new CardDTO();
    }
}
