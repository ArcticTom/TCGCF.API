using System;
using System.Collections.Generic;

namespace TCGCF.API.Models
{
    public class ImportSet
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string ReleaseDate { get; set; }
        public string Type { get; set; }
        public List<ImportCard> Cards { get; set; }

        public int NumberOfCards() {
            return Cards.Count;
        }
    }
}