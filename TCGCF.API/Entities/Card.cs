using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCGCF.API.Entities
{
    public class Card
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public string Number { get; set; }

        [MaxLength(1000)]
        public string FlavorText { get; set; }

        [MaxLength(1000)]
        public string RulesText { get; set; }

        [Required]
        [MaxLength(200)]
        public string Artist { get; set; }

        [DataType(DataType.Url)]
        [Required]
        public string Image { get; set; }

        [MaxLength(50)]
        public string ManaCost { get; set; }

        [Required]
        public int CMC { get; set; }

        [Required]
        public string Power { get; set; }

        [Required]
        public string Toughness { get; set; }

        [Required]
        public int Loyalty { get; set; }

        [Required]
        public int LinkedCard { get; set; }

        public ICollection<CardsInDeck> CardsInDeck { get; set; } = new List<CardsInDeck>();

        public ICollection<CardSuperType> CardSuperType { get; set; } = new List<CardSuperType>();

        public ICollection<CardType> CardType { get; set; } = new List<CardType>();

        public ICollection<CardSubType> CardSubType { get; set; } = new List<CardSubType>();

        public ICollection<Color> Color { get; set; } = new List<Color>();

        public ICollection<ColorIdentity> ColorIdentity { get; set; } = new List<ColorIdentity>();

        [ForeignKey("SetId")]
        public Set Set { get; set; }
        public int SetId { get; set; }

        public Legality Legality { get; set; } = new Legality();

        public CardLayout CardLayout { get; set; } = new CardLayout();

        public Rarity Rarity { get; set; } = new Rarity();

        public Language Language { get; set; } = new Language();

    }
}
