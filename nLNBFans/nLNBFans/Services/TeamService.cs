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
    public class TeamService
    {

        public async Task<List<Team>> GetTeams()
        {
            var teams = new List<Team>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                var response = await client.GetAsync("api/equipo/colectivo/temporadas/2016");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    teams = JsonConvert.DeserializeObject<List<Team>>(content);
                }
                else
                {
                    return teams;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
                return teams;
            }

            return teams;
        }

        public async Task<List<TeamInformation>> GetTeamInformation(int idTeam)
        {
            var teamInformation = new List<TeamInformation>();
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(UrlBaseRest.GetUrlBaseRest());
                var response = await client.GetAsync($"api/equipo/staff/{idTeam}");

                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    teamInformation = JsonConvert.DeserializeObject<List<TeamInformation>>(content);
                }
                else
                {
                    return teamInformation;
                }
            }
            catch (Exception e)
            {
                string nada = e.Message;
                return teamInformation;
            }

            return teamInformation;
        }
    }
}
