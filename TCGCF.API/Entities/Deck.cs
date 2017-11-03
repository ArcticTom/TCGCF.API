using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCGCF.API.Entities
{
    public class Deck
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [MaxLength(200)]
        [MinLength(5)]
        public string Creator { get; set; }

        public ICollection<CardsInDeck> Cards { get; set; } = new List<CardsInDeck>();

        //automatic update checking
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
