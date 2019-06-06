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
    public class GameDetailsService
    {
      
        public async Task<GameScoreTime> GetGameScoreTime(int idCalendario)
        {
            var gameScoreTime = new GameScoreTime();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                var response = await client.GetAsync($"api/boxscores/juegoscoretime/{idCalendario}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    gameScoreTime = JsonConvert.DeserializeObject<GameScoreTime>(content);
                }
                else
                {
                    return gameScoreTime;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
                return gameScoreTime;
            }

            return gameScoreTime;
        }

        public async Task<List<GameTotals>> GetGameTotals(int idCalendario)
        {
            var gameTotals = new List<GameTotals>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                var response = await client.GetAsync($"api/boxscores/{idCalendario}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    gameTotals = JsonConvert.DeserializeObject<List<GameTotals>>(content);
                }
                else
                {
                    return gameTotals;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
                return gameTotals;
            }

            return gameTotals;
        }
    }
}
