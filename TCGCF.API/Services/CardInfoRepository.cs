using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TCGCF.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace TCGCF.API.Services
{
    public class CardInfoRepository : ICardInfoRepository
    {
        private CardInfoContext _context;

        public CardInfoRepository(CardInfoContext context)
        {
            _context = context;
        }

        public bool SetExists(string abbr)
        {
            return _context.Sets.Any(c => c.Abbreviation == abbr);
        }

        public bool DeckExists(int deckId)
        {
            return _context.Decks.Any(c => c.Id == deckId);
        }

        public Card GetCardForDeck(int deckId, int cardId)
        {
            var cardsInDeck = _context.CardsInDeck.Where(c => c.CardId == cardId && c.DeckId == deckId).FirstOrDefault();
            if (cardsInDeck != null)
            {
                return _context.Cards.Include(c => c.Legality).Where(c => c.Id == cardsInDeck.CardId).FirstOrDefault();
            }
            return null;
        }

        public Card GetCardForSet(string abbr, int prtNum)
        {
            var set = _context.Sets.Where(c => c.Abbreviation == abbr).FirstOrDefault();

            return _context.Cards.Include(c => c.Legality).Where(c => c.PrintNumber == prtNum && c.SetId == set.Id).FirstOrDefault();
        }

        public IEnumerable<Card> GetCardsForDeck(int deckId)
        {
            var cardsInDeck = _context.CardsInDeck.Where(c => c.DeckId == deckId).ToList();

            if (cardsInDeck != null)
            {
                var list = cardsInDeck.Select(c => c.CardId).ToList();
                return _context.Cards.Include(c => c.Legality).Where(c => list.Contains(c.Id)).OrderBy(c => c.Id).ToList();
            }
            return null;
        }

        public IEnumerable<Card> GetCardsForSet(string abbr)
        {
            var set = _context.Sets.Where(c => c.Abbreviation == abbr).FirstOrDefault();

            return _context.Cards.Include(c => c.Legality).Where(c => c.SetId == set.Id).OrderBy(c => c.Id).ToList();
        }

        public Deck GetDeck(int Id, bool includeCards)
        {
            if (includeCards)
            {

                return _context.Decks.Include("Cards.Card").Include("Cards.Card.Legality").Where(c => c.Id == Id).FirstOrDefault();
            }

            return _context.Decks.Where(c => c.Id == Id).FirstOrDefault();
        }

        public IEnumerable<Deck> GetDecks()
        {
            return _context.Decks.OrderBy(c => c.Name).ToList();
        }

        public Set GetSet(string Abbr, bool includeCards)
        {
            if (includeCards)
            {
                return _context.Sets.Include(c => c.Cards).ThenInclude(c => c.Legality).Where(c => c.Abbreviation == Abbr).FirstOrDefault();
            }

            return _context.Sets.Where(c => c.Abbreviation == Abbr).FirstOrDefault();
        }

        public IEnumerable<Set> GetSets()
        {
            return _context.Sets.OrderBy(c => c.Id).ToList();
        }

        public void AddCardToDeck(int deckId, CardsInDeck card)
        {
            var deck = GetDeck(deckId, true);

            deck.Cards.Add(card);
        }

        public void AddCardsToDeck(int deckId, IEnumerable<CardsInDeck> cardsToAdd)
        {
            var deck = GetDeck(deckId, true);
            foreach(var card in cardsToAdd)
            {
                if(deckId == card.DeckId)
                {
                    deck.Cards.Add(card);
                }
            }
        }

        /*
        public void UpdateMultiple()
        {
            var items = _context.Decks.ToList();
            items.ForEach(s => s.Name += "Test");
            OR _context.UpdateRange();

            To execute own SQL statements
            _context.FromSql("SELECT * FROM Decks").ToList();
            _context.Database.ExecuteSqlCommand("UPDATE Decks SET Name = 'Test'");
        }
        */

        public void AddDeck(Deck deck)
        {
            _context.Decks.Add(deck);
        }

        public async Task<bool> SaveChanges()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void DeleteDeck(Deck deck)
        {
            _context.Remove(deck);
        }
    }
}
