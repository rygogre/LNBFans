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
    public class ScheduleService
    {
        public ScheduleService()
        {

        }

        public async Task<List<Schedule>> GetSchedule(string  year, string etapa)
        {
            var schedule = new List<Schedule>();
            try
            {
                //string year = string.Empty;
                //string etapa = string.Empty;

                //var temporadaActual = new TemporadaActual();
                //temporadaActual = await Helpers.SettingHelper.TemporadaActual();

                //if (temporadaActual != null)
                //{
                //    year = temporadaActual.Temporada;
                //    etapa = temporadaActual.Etapa;
                //}

                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                var response = await client.GetAsync($"api/calendarios/1/{year}/0/{etapa}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    schedule = JsonConvert.DeserializeObject<List<Schedule>>(content);
                }
                else
                {
                    return schedule;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
                return schedule;
            }

            return schedule;
        }
    }
}
