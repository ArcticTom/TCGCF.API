using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCGCF.API.Entities
{
    public class Legality
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public bool Vintage { get; set; }
        [Required]
        public bool Legacy { get; set; }
        [Required]
        public bool Pauper { get; set; }
        [Required]
        public bool Commander { get; set; }
        [Required]
        public bool Modern { get; set; }
        [Required]
        public bool Standard { get; set; }
        [Required]
        public bool Frontier { get; set; }
        public bool? Arena { get; set; }

        [ForeignKey("CardId")]
        public Card Card { get; set; }
        public int CardId { get; set; }
    }
}
