using System.Collections.Generic;

namespace TCGCF.API.Models
{
    public class SetDTO
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }

        public int NumberOfCards
        {
            get
            {
                return Cards.Count;
            }
        }

        public ICollection<CardNoIdDTO> Cards { get; set; } = new List<CardNoIdDTO>();
    }
}
