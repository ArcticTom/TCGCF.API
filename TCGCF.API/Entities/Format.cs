using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCGCF.API.Entities
{
    public class Format
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(200)]
        public string Name { get; set; }

        [Required]
        public int NumberOfCards { get; set; }

        [Required]
        public int CopyLimit { get; set; }

        [Required]
        [MaxLength(100)]
        public string Category { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [ForeignKey("GameId")]
        public Game Game { get; set; }
        public int GameId { get; set; }

        //automatic update checking
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
