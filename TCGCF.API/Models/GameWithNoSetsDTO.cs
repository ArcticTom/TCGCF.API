using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TCGCF.API.Models
{
    public class GameWithNoSetsDTO
    {
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public DateTime Published { get; set; }
        [DataType(DataType.Url)]
        public string Website { get; set; }
        public string Publisher { get; set; }
        public string Description { get; set; }
        public bool AvailableOnPaper { get; set; }
        public bool AvailableOnPC { get; set; }
        public bool AvailableOnMobile { get; set; }
        public bool AvailableOnConsole { get; set; }
        
        public ICollection<FormatDTO> Formats { get; set; } = new List<FormatDTO>();

    }
}
