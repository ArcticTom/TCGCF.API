using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCGCF.API.Entities
{
    public class Set
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(3)]
        public string Abbreviation { get; set; }

        public ICollection<Card> Cards { get; set; } = new List<Card>();

        //automatic update checking
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
