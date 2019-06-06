using nLNBFans.Helpers;
using nLNBFans.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace nLNBFans.Services
{
    public class GameDayService
    {
        public GameDayService()
        {
           //constructor...
        }

        public async Task<List<GameDay>> GetGameDay(string fecha)
        {
            var gameDay = new List<GameDay>();
            try
            {
                string urlClient = "api/calendario";
                var client = new HttpClient();                
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                if (fecha != null)
                    urlClient = $"api/calendarios/1/2016/{fecha}/SR";

                var response = await client.GetAsync(urlClient);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    gameDay = JsonConvert.DeserializeObject<List<GameDay>>(content);                    
                }
                else
                {                   

                    return gameDay;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
            }

            return gameDay;
        }
    }
}
