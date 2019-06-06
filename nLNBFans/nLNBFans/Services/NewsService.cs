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
    public class NewsService
    {
        public NewsService()
        {

        }

        public async Task<List<News>> GetNews()
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                var response = await client.GetAsync("api/news");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    var news = JsonConvert.DeserializeObject<List<News>>(content);

                    return news;
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
