using AutoMapper;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using TCGCF.API.Models;
using TCGCF.API.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace TCGCF.API.Services
{
    public class DBUpdate
    {
        
        public static async Task<string> Begin(IConfiguration config) {

            var jsonresult = "";
  
                //create dbcontext options from config
                var optionsBuilder = new DbContextOptionsBuilder<CardInfoContext>();
                optionsBuilder.UseNpgsql(config["connectionStrings:TCGCardFetcher"]);

                using (CardInfoContext context = new CardInfoContext(optionsBuilder.Options))
                {
                    //set base game data
                    List<Game> _sample = new List<Game>
                    {
                        new Game() {
                            Name = "Magic: The Gathering",
                            Abbreviation = "MTG",
                            Published = DateTime.Parse("1993-08-05 00:00:00.000000"),
                            Publisher = "Wizards of the Coast",
                            Website = "https://magic.wizards.com/en",
                            Description = "Magic can be played by two or more players in various formats, which fall into two categories: constructed and limited. Limited formats involve players building a deck spontaneously out of a pool of random cards with a minimum deck size of 40 cards. In constructed, players create decks from cards they own, usually 60 cards with no more than 4 of any given card. Each game represents a battle between wizards known as planeswalkers, who employ spells, artifacts, and creatures depicted on individual Magic cards to defeat their opponents.",
                            AvailableOnConsole = true,
                            AvailableOnMobile = true,
                            AvailableOnPaper = true,
                            AvailableOnPC = true,
                            Formats = new List<Format>
                            { 
                                new Format() {
                                    Name = "Standard",
                                    NumberOfCards = 60,
                                    CopyLimit = 4,
                                    Category = "Constructed",
                                    Description = ""
                                },
                                new Format() {
                                    Name = "Modern",
                                    NumberOfCards = 60,
                                    CopyLimit = 4,
                                    Category = "Constructed",
                                    Description = ""
                                },
                                new Format() {
                                    Name = "Legacy",
                                    NumberOfCards = 60,
                                    CopyLimit = 4,
                                    Category = "Constructed",
                                    Description = ""
                                },
                                new Format() {
                                    Name = "Vintage",
                                    NumberOfCards = 60,
                                    CopyLimit = 4,
                                    Category = "Constructed",
                                    Description = ""
                                },
                                new Format() {
                                    Name = "Commander",
                                    NumberOfCards = 100,
                                    CopyLimit = 1,
                                    Category = "Casual",
                                    Description = ""
                                },
                                new Format() {
                                    Name = "Sealed",
                                    NumberOfCards = 40,
                                    CopyLimit = 40,
                                    Category = "Limited",
                                    Description = ""
                                },
                                new Format() {
                                    Name = "Draft",
                                    NumberOfCards = 40,
                                    CopyLimit = 40,
                                    Category = "Limited",
                                    Description = ""
                                },
                                new Format() {
                                    Name = "Pauper",
                                    NumberOfCards = 60,
                                    CopyLimit = 4,
                                    Category = "Casual",
                                    Description = ""
                                },
                                new Format() {
                                    Name = "Frontier",
                                    NumberOfCards = 60,
                                    CopyLimit = 4,
                                    Category = "Casual",
                                    Description = ""
                                },
                                new Format() {
                                    Name = "Arena",
                                    NumberOfCards = 60,
                                    CopyLimit = 4,
                                    Category = "Constructed",
                                    Description = ""
                                }
                            }
                        }
      
                    };

                    //empty all data before records are added
                    context.Database.ExecuteSqlCommand("TRUNCATE TABLE \"Games\" RESTART IDENTITY CASCADE;");

                    context.Games.AddRange(_sample);
                    
                    await context.SaveChangesAsync();
                }

                //read set data from json file
                JObject jObject = JObject.Load(new JsonTextReader(File.OpenText("enrichData.json")));
                JArray resources = (JArray)jObject["set"];

                //fetch more data for each set from online, bind to the model and enrich it
                foreach (var set in resources)
                {
                    jsonresult = await DownloadJSON("https://mtgjson.com/json/"+ set["id"].ToString() +"-x.json");

                    if(jsonresult != null) {

                        using (CardInfoContext context = new CardInfoContext(optionsBuilder.Options))
                        {

                            var modelresult = ConvertToModel(jsonresult);
                            var enrichedModel = EnrichModel(modelresult);

                            //add set to context
                            context.Sets.Add(enrichedModel);

                            //set power and toughness even when null
                            var nulls = enrichedModel.Cards.Where(item => item.Power == null);
                            foreach(Card card in nulls) {
                                card.Power = " ";
                                card.Toughness = " ";
                            }

                            //update cards in context
                            context.Cards.UpdateRange(nulls);
                    
                            await context.SaveChangesAsync();
                        }
                    }
                }

            return jsonresult;
        }
        
        
        public static async Task<string> DownloadJSON(string url) {

            using (var client = new HttpClient())
            {
                using (var result = await client.GetAsync(url))
                {
                    //if successful
                    if (result.IsSuccessStatusCode)
                    {
                        var jsonresult = await result.Content.ReadAsStringAsync();
                        return jsonresult;
                    }
                }
            }
            return null;
        }

        public static Set ConvertToModel(string json) {

                //deserialize json into import model
                var importSet = JsonConvert.DeserializeObject<ImportSet>(json);

                //use automapper to map into correct model
                var convertedModel = Mapper.Map<Set>(importSet);

                //TODO beautify
                //Manual mapping that I couldn't do in automapper
                #region manual mapping
                foreach(ImportCard card in importSet.Cards) {

                    //verify that the card has a number
                    if(card.MciNumber != null) {

                        //first to allow for duplicates in data
                        var newCard = convertedModel.Cards.First(x => x.Number == card.MciNumber);

                        if(card.Supertypes != null) {

                            foreach(string supertype in card.Supertypes) {

                                var super = new CardSuperType();
                                super.Name = supertype;
                                newCard.CardSuperType.Add(super);
                            } 
                        }

                        if(card.Types != null) {

                            foreach(string type in card.Types) {

                                var newType = new CardType();
                                newType.Name = type;
                                newCard.CardType.Add(newType);
                            } 
                        }

                        if(card.Subtypes != null) {

                            foreach(string subtype in card.Subtypes) {

                                var sub = new CardSubType();
                                sub.Name = subtype;
                                newCard.CardSubType.Add(sub);
                            } 
                        }

                        if(card.ColorIdentity != null) {

                            foreach(string coloridentity in card.ColorIdentity) {

                                var identity = new ColorIdentity();
                                Enum.TryParse(coloridentity, out EColorIdentity ciEnum);
                                identity.Name = ciEnum;
                                newCard.ColorIdentity.Add(identity);
                            } 
                        }

                        if(card.Colors != null) {

                            foreach(string color in card.Colors) {

                                var c = new Color();
                                Enum.TryParse(color, out EColor cEnum);
                                c.Name = cEnum;
                                newCard.Color.Add(c);
                            } 
                        }

                        if(card.Rarity != null) {

                            var r = new Rarity();
                            Enum.TryParse(card.Rarity, out ERarity rEnum);
                            r.Name = rEnum;
                            newCard.Rarity = r;
                        }

                        if(card.Legalities != null) {

                            var legal = new Legality();

                            foreach(ImportLegality legality in card.Legalities) {

                                switch(legality.Format) {
                                    case "Vintage": 
                                        if(legality.Legality == "Legal") {
                                            legal.Vintage = true;
                                        }
                                    break;
                                    case "Legacy":
                                        if(legality.Legality == "Legal") {
                                            legal.Legacy = true;
                                        }
                                    break;
                                    case "Commander":
                                        if(legality.Legality == "Legal") {
                                            legal.Commander = true;
                                        }
                                    break;
                                    case "Modern":
                                        if(legality.Legality == "Legal") {
                                            legal.Modern = true;
                                        }
                                    break;
                                    case "Standard":
                                        if(legality.Legality == "Legal") {
                                            legal.Standard = true;
                                        }
                                    break;
                                    case "Arena":
                                        if(legality.Legality == "Legal") {
                                            legal.Arena = true;
                                        }
                                    break;
                                    default:
                                    break;

                                }
                            } 

                            if(newCard.Rarity.Name == 0) {
                                legal.Pauper = true;
                            }

                            newCard.Legality = legal;

                        }
                        
                        if(card.Power == null) {

                            newCard.Power = " ";

                        }

                        if(card.Toughness == null) {

                            newCard.Toughness = " ";
                        }

                    }

                }
                #endregion

                return convertedModel;

        }

        public static Set EnrichModel(Set model) {

                //enrich set data from json
                JObject jObject = JObject.Load(new JsonTextReader(File.OpenText("enrichData.json")));
                JArray resources = (JArray)jObject["set"];
                //get each set based on the abbreviation
                foreach (var set in resources.Where(obj => obj["id"].Value<string>() == model.Abbreviation))
                {
                    model.Story = set["story"].ToString();
                    model.Symbol = set["symbol"].ToString();
                    model.GameId = 1;
                }

            return model;
        }
    }
}