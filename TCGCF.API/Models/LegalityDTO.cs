namespace TCGCF.API.Models
{
    public class LegalityDTO
    {
        public bool Vintage { get; set; }
        public bool Legacy { get; set; }
        public bool Pauper { get; set; }
        public bool Commander { get; set; }
        public bool Modern { get; set; }
        public bool Standard { get; set; }
        public bool Frontier { get; set; }
        public bool? Arena { get; set; }
    }
}
