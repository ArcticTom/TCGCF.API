using System.Collections.Generic;
using System.Threading.Tasks;
using TCGCF.API.Entities;

namespace TCGCF.API.Services
{
    public interface ICardInfoRepository
    {
        //get all sets
        IEnumerable<Set> GetSets(string abbr);

        //get specific set
        Set GetSet(string abbr, string setAbbr, bool includeCards);

        //get all cards for set
        IEnumerable<Card> GetCardsForSet(string abbr, string setAbbr);

        //get specific card for set
        Card GetCardForSet(string abbr, string setAbbr, int prtNum);

        //get all decks
        IEnumerable<Deck> GetDecks();

        //get specific deck
        Deck GetDeck(int Id, bool includeCards);

        //get all cards for deck
        IEnumerable<Card> GetCardsForDeck(int deckId);

        //get specific card for deck
        Card GetCardForDeck(int deckId, int cardId);

        //check if set exists
        bool SetExists(string abbr, string setAbbr);

        //check if deck exists
        bool DeckExists(int deckId);

        //add card to specific deck
        void AddCardToDeck(int deckId, CardsInDeck card);

        //add cards to specific deck
        void AddCardsToDeck(int deckId, IEnumerable<CardsInDeck> cards);

        //add new deck
        void AddDeck(Deck deck);

        //delete deck
        void DeleteDeck(Deck deck);

        //save changes to database
        Task<bool> SaveChanges();

        //get all games
        IEnumerable<Game> GetGames();

        //get specific set
        Game GetGame(string abbr);
    }
}
