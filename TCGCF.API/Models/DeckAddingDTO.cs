using System.ComponentModel.DataAnnotations;

namespace TCGCF.API.Models
{
    public class DeckAddingDTO
    {
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        [MinLength(5)]
        public string Creator { get; set; }

    }
}
