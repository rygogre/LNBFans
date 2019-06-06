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
    public class SettingService
    {

        public async Task<TemporadaActual> GetTemporadaActual()
        {
            var temporadaActual = new TemporadaActual();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                var response = await client.GetAsync("api/season");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    temporadaActual = JsonConvert.DeserializeObject<TemporadaActual>(content);

                    return temporadaActual;
                }
                else
                {
                    var nada = response.StatusCode;

                    return temporadaActual;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
            }

            return temporadaActual;
        }

    }
}
