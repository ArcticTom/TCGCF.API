namespace TCGCF.API.Models
{
    public class CardNoIdDTO
    {

        public string Name { get; set; }
        public string Description { get; set; }
        public int PrintNumber { get; set; }

        public LegalityDTO Legality { get; set; } = new LegalityDTO();

    }
}
