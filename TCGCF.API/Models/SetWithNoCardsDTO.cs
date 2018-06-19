using System;
using TCGCF.API.Entities;

namespace TCGCF.API.Models
{
    public class SetWithNoCardsDTO
    {
        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

        public string Story { get; set; }

        public string Symbol { get; set; }

        public int NumberOfCards { get; set; }

        public string Abbreviation { get; set; }

        public SetTypeDTO SetType { get; set; } = new SetTypeDTO();

    }
}
