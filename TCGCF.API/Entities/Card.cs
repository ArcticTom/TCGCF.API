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
        public int PrintNumber { get; set; }
        [MaxLength(1000)]
        public string Description { get; set; }

        public ICollection<CardsInDeck> CardsInDeck { get; set; } = new List<CardsInDeck>();

        [ForeignKey("SetId")]
        public Set Set { get; set; }
        public int SetId { get; set; }

        public Legality Legality { get; set; } = new Legality();

        //automatic update checking
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
