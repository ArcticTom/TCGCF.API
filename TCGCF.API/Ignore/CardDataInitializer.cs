using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCGCF.API.Entities;

namespace TCGCF.API.Ignore
{
    public class CardDataInitializer
    {
        private CardInfoContext _ctx;

        public CardDataInitializer(CardInfoContext ctx)
        {
            _ctx = ctx;
        }

        public async Task Seed()
        {
            //check if sets exist
            if (!_ctx.Sets.Any())
            {
                // Add Data
                _ctx.AddRange(_sample);
                await _ctx.SaveChangesAsync();
            }
        }

    //set sample data for testing
    List<Set> _sample = new List<Set>
    {
      new Set()
      {
        Name = "Ixalan",
        Abbreviation = "XLN",
        Cards = new List<Card>
        {
          new Card()
          {
            Name = "Adanto Vanguard",
            Description = "",
            PrintNumber = 1,
            Legality = new Legality()
            {
                Commander =  true,
                Frontier = true,
                Legacy = true,
                Modern = true,
                Pauper = false,
                Standard = true,
                Vintage = true
            }
          },
          new Card()
          {
            Name = "Ashes of the Abhorrent",
            Description = "\"Let no trace of the vampires' foulness remain.\"",
            PrintNumber = 2,
            Legality = new Legality()
            {
                Commander =  true,
                Frontier = true,
                Legacy = true,
                Modern = true,
                Pauper = false,
                Standard = true,
                Vintage = true
            }
          }
        }
      },
      new Set()
      {
        Name = "Shadows over Innistrad",
        Abbreviation = "SOI",
        Cards = new List<Card>
        {
          new Card()
          {
            Name = "Tireless Tracker",
            Description = "",
            PrintNumber = 233,
            Legality = new Legality()
            {
                Commander =  true,
                Frontier = true,
                Legacy = true,
                Modern = true,
                Pauper = false,
                Standard = false,
                Vintage = true
            }
          },
          new Card()
          {
            Name = "Traverse the Ulvenwald",
            Description = "",
            PrintNumber = 234,
            Legality = new Legality()
            {
                Commander =  true,
                Frontier = true,
                Legacy = true,
                Modern = true,
                Pauper = false,
                Standard = false,
                Vintage = true
            }
          }
        }
      }
    };

    }
}
