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
    public class PlayerService
    {
       
        public PlayerService()
        {

        }


        public async Task<List<Players>> GetPlayers()
        {
            var players = new List<Players>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                var response = await client.GetAsync("api/players");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    players = JsonConvert.DeserializeObject<List<Players>>(content);
                }
                else
                {                   
                    return players;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
                return players;
            }

            return players;
        }

        public async Task<List<Players>> GetPlayersTeam(int idTeam)
        {
            var players = new List<Players>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                var response = await client.GetAsync($"api/players/equipo/{idTeam}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    players = JsonConvert.DeserializeObject<List<Players>>(content);
                }
                else
                {
                    return players;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
                return players;
            }

            return players;
        }

        public async Task<Players> GetPlayerID(int idPlayer)
        {
            var players = new Players();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                var response = await client.GetAsync($"api/players/{idPlayer}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    players = JsonConvert.DeserializeObject<Players>(content);
                }
                else
                {
                    return players;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
                return players;
            }

            return players;
        }

        public async Task<List<PlayerStats>> GetPlayerStats(int idPlayer, string etapa)
        {
            var playersStats = new List<PlayerStats>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                var response = await client.GetAsync($"api/players/{idPlayer}/{etapa}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    playersStats = JsonConvert.DeserializeObject<List<PlayerStats>>(content);
                }
                else
                {
                    return playersStats;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
                return playersStats;
            }

            return playersStats;
        }
    }
}
