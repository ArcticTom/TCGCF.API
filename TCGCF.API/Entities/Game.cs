using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

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

        public ICollection<Set> Sets { get; set; } = new List<Set>();

        //automatic update checking
        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
