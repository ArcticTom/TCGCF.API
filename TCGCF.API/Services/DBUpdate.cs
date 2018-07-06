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

namespace TCGCF.API.Services
{
    public class DBUpdate
    {
        
        public static async Task<string> Begin(string url) {

            var jsonresult = await DownloadJSON(url);

            var modelresult = ConvertToModel(jsonresult);

            var enrichedModel = EnrichModel(modelresult);

            var dbresult = await SaveToDB(enrichedModel);

            return jsonresult;
        }
        
        
        public static async Task<string> DownloadJSON(string url) {

            using (var client = new HttpClient())
            {
                using (var result = await client.GetAsync(url))
                {
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

            try
            {
                var importSet = JsonConvert.DeserializeObject<ImportSet>(json);

                var convertedModel = Mapper.Map<Set>(importSet);

                return convertedModel;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static Set EnrichModel(Set model) {

            JObject jObject = JObject.Load(new JsonTextReader(File.OpenText("enrichData.json")));
            JArray resources = (JArray)jObject["set"];
            foreach (var set in resources.Where(obj => obj["id"].Value<string>() == model.Abbreviation))
            {
                model.Story = set["story"].ToString();
                model.Symbol = set["symbol"].ToString();
                model.GameId = 3;
            }

            return model;
        }

        public static async Task<bool> SaveToDB(Set model) {

            

            return true;
        }
    }
}