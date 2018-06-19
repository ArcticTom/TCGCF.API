using System;
using System.Collections.Generic;

namespace TCGCF.API.Models
{
    public class CardNoIdDTO
    {

        public string Name { get; set; }

        public int PrintNumber { get; set; }

        public string FlavorText { get; set; }

        public string RulesText { get; set; }

        public string Artist { get; set; }

        public string Image { get; set; }

        public string ManaCost { get; set; }

        public int CMC { get; set; }

        public int Power { get; set; }

        public int Toughness { get; set; }

        public int Loyalty { get; set; }

        public int LinkedCard { get; set; }

        public LegalityDTO Legality { get; set; } = new LegalityDTO();

        public CardLayoutDTO CardLayout { get; set; } = new CardLayoutDTO();

        public RarityDTO Rarity { get; set; } = new RarityDTO();

        public LanguageDTO Language { get; set; } = new LanguageDTO();

        public ICollection<CardSuperTypeDTO> CardSuperType { get; set; } = new List<CardSuperTypeDTO>();

        public ICollection<CardTypeDTO> CardType { get; set; } = new List<CardTypeDTO>();

        public ICollection<CardSubTypeDTO> CardSubType { get; set; } = new List<CardSubTypeDTO>();

        public ICollection<ColorDTO> Color { get; set; } = new List<ColorDTO>();

        public ICollection<ColorIdentityDTO> ColorIdentity { get; set; } = new List<ColorIdentityDTO>();

    }
}
