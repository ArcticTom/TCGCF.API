using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TCGCF.API.Entities
{
    public class Game
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

        [Required]
        public DateTime Published { get; set; }

        [DataType(DataType.Url)]
        [Required]
        public string Website { get; set; }

        [Required]
        [MaxLength(100)]
        public string Publisher { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Description { get; set; }

        [Required]
        public bool AvailableOnPaper { get; set; }

        [Required]
        public bool AvailableOnPC { get; set; }

        [Required]
        public bool AvailableOnMobile { get; set; }

        [Required]
        public bool AvailableOnConsole { get; set; }

        public ICollection<Set> Sets { get; set; } = new List<Set>();

        public ICollection<Format> Formats { get; set; } = new List<Format>();

        //automatic update checking
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
