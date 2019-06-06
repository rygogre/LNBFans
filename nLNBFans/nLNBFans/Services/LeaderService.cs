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
    public class LeaderService
    {
        public LeaderService()
        {

        }



        public async Task<List<Leader>> GetLeaders(string equipo, string area, string circuito, string temporada, string etapa)
        {
            var leaders = new List<Leader>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                string url = "";

                if (equipo == "t" || equipo == "n" || equipo == "s")
                    url = $"api/lideres/{area}/{circuito}/{temporada}/{etapa}";
                else
                    url = $"api/lideres/equipos/{equipo}/{area}/{temporada}/{etapa}";

                var response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    leaders = JsonConvert.DeserializeObject<List<Leader>>(content);
                }
                else
                {
                    return leaders;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
                return leaders;
            }

            return leaders;
        }
    }
}
