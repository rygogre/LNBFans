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
    public class StandingService
    {
        public StandingService()
        {
            
        }

        public async Task<List<Standing>> GetStanding(string circuito)
        {
            try
            {
                string year = "2018";  //Helpers.Settings.TemporadaSettings;
                string etapa = "SR"; // Helpers.Settings.EtapaSettings;

                //var temporadaActual = new TemporadaActual();
                //temporadaActual = await Helpers.SettingHelper.TemporadaActual();

               

                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                var response = await client.GetAsync($"api/posiciones/{year}/{etapa}/{circuito}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var stading = JsonConvert.DeserializeObject<List<Standing>>(content);

                    return stading;
                }
                else
                {
                    var nada = response.StatusCode;

                    return null;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
            }

            return null;
        }
    }
}
