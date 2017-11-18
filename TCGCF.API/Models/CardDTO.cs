namespace TCGCF.API.Models
{
    public class CardDTO
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string FlavorText { get; set; }
        public int PrintNumber { get; set; }

        public LegalityDTO Legality { get; set; } = new LegalityDTO();
    }
}
