using System.Collections.Generic;
using System.Threading.Tasks;
using TCGCF.API.Entities;

namespace TCGCF.API.Services
{
    public interface ICardInfoRepository
    {
        //get all sets
        IEnumerable<Set> GetSets();

        //get specific set
        Set GetSet(string Abbr, bool includeCards);

        //get all cards for set
        IEnumerable<Card> GetCardsForSet(string abbr);

        //get specific card for set
        Card GetCardForSet(string abbr, int prtNum);

        //get all decks
        IEnumerable<Deck> GetDecks();

        //get specific deck
        Deck GetDeck(int Id, bool includeCards);

        //get all cards for deck
        IEnumerable<Card> GetCardsForDeck(int deckId);

        //get specific card for deck
        Card GetCardForDeck(int deckId, int cardId);

        //check if set exists
        bool SetExists(string abbr);

        //check if deck exists
        bool DeckExists(int deckId);

        //add card to specific deck
        void AddCardToDeck(int deckId, CardsInDeck card);

        //add new deck
        void AddDeck(Deck deck);

        //delete deck
        void DeleteDeck(Deck deck);

        //save changes to database
        Task<bool> SaveChanges();
    }
}
