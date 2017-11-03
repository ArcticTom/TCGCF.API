using System.Collections.Generic;

namespace TCGCF.API.Models
{
    public class DeckDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Creator { get; set; }

        public ICollection<CardsInDeckDTO> Cards { get; set; } = new List<CardsInDeckDTO>();
    }
}
