using System;

namespace TCGCF.API.Models
{
    public class FormatDTO
    {
        public string Name { get; set; }

        public int NumberOfCards { get; set; }

        public int CopyLimit { get; set; }

        public string Category { get; set; }

        public string Description { get; set; }

    }
}