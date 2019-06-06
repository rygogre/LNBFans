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
    public class BoxScoreService
    {
        public BoxScoreService()
        {

        }

        public async Task<List<Boxscore>> GetBoxScore(int idCalendario, int idTeam)
        {
            var boxScore = new List<Boxscore>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                var response = await client.GetAsync($"api/boxscores/{idCalendario}/{idTeam}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    boxScore = JsonConvert.DeserializeObject<List<Boxscore>>(content);
                }
                else
                {
                    return boxScore;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
                return boxScore;
            }

            return boxScore;
        }
    }
}
