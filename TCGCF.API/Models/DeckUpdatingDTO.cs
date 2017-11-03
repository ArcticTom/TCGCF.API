using System.ComponentModel.DataAnnotations;

namespace TCGCF.API.Models
{
    public class DeckUpdatingDTO
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
