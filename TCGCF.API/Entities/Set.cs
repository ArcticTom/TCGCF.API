using System;
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
        public DateTime ReleaseDate { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Story { get; set; }

        [Required]
        [MaxLength(100)]
        public string Symbol { get; set; }

        [Required]
        public int NumberOfCards { get; set; }

        [Required]
        [MaxLength(10)]
        [MinLength(3)]
        public string Abbreviation { get; set; }

        public ICollection<Card> Cards { get; set; } = new List<Card>();

        public SetType SetType { get; set; } = new SetType();

        [ForeignKey("GameId")]
        public Game Game { get; set; }
        public int GameId { get; set; }

    }
}
