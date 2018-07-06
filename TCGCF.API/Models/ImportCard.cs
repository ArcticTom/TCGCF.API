using System;
using System.Collections.Generic;

namespace TCGCF.API.Models
{
    public class ImportCard
    {

        public string Artist { get; set; }
        public int Cmc { get; set; }
        //public List<string> ColorIdentity { get; set; }
        //public List<string> Colors { get; set; }
        public string Flavor { get; set; }
        public string Layout { get; set; }
        //public List<ImportLegality> Legalities { get; set; }
        public string ManaCost { get; set; }
        public string Name { get; set; }
        public string Number { get; set; }
        //public string Rarity { get; set; }
        public string Text { get; set; }
        //public List<string> Types { get; set; }
        public string Power { get; set; }
        //public List<string> Subtypes { get; set; }
        public string Toughness { get; set; }
        //public List<string> Supertypes { get; set; }
        public int? Loyalty { get; set; }

    }
}