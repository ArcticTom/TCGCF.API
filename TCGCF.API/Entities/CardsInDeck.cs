using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCGCF.API.Entities
{
    public class CardsInDeck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [ForeignKey("DeckId")]
        public Deck Deck { get; set; }
        public int DeckId { get; set; }

        [ForeignKey("CardId")]
        public Card Card { get; set; }
        public int CardId { get; set; }

    }
}
