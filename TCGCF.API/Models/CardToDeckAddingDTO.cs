using System.ComponentModel.DataAnnotations;

namespace TCGCF.API.Models
{
    public class CardToDeckAddingDTO
    {
        [Required]
        public int CardId { get; set; }
        [Required]
        public int DeckId { get; set; }
    }
}
