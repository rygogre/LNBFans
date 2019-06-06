using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using nLNBFans.Helpers;
using nLNBFans.Models;
using Newtonsoft.Json;

namespace nLNBFans.Services
{
    public class PublicidadService
    {
        public PublicidadService()
        {
            //constructor
        }

        public async Task<List<Publicidad>> GetPublicidad()
        {
            var publicidad = new List<Publicidad>();
            try
            {
                string urlClient = "api/Publicidad";
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                
                var response = await client.GetAsync(urlClient);

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    publicidad = JsonConvert.DeserializeObject<List<Publicidad>>(content);
                }
                else
                {

                    return publicidad;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
            }

            return publicidad;
        }
    }
}
