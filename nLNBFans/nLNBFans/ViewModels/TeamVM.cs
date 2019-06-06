using nLNBFans.Models;
using nLNBFans.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace nLNBFans.ViewModels
{
    public class TeamVM : INotifyPropertyChanged
    {
        private bool isBusy;
        private List<Team> allTeams;
        private List<Team> teamsNorte;
        private List<Team> teamsSuroeste;
        private List<TeamInformation> teamInformation;
        private string publicidadHTML;

        public List<Team> Teams
        {
            get { return allTeams; }
            set { allTeams = value; OnPropertyChanged(); }
        }

        public List<Team> TeamsNorte
        {
            get { return teamsNorte; }
            set { teamsNorte = value; OnPropertyChanged(); }
        }

        public List<Team> TeamsSuroeste
        {
            get { return teamsSuroeste; }
            set { teamsSuroeste = value; OnPropertyChanged(); }
        }
        public List<TeamInformation> TeamInformation
        {
            get { return teamInformation; }
            set { teamInformation = value; OnPropertyChanged(); }
        }
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        public string PublicidadHTML
        {
            get { return publicidadHTML; }
            set { publicidadHTML = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public TeamVM()
        {
            GetAllTeams();
            GetPublicidad();
        }

        public TeamVM(int idTeam)
        {
            GetTeamInformation(idTeam);
        }

        public async void GetAllTeams()
        {
            IsBusy = true;
            Teams = new List<Team>();
            TeamsNorte = new List<Team>();
            TeamsSuroeste = new List<Team>();

            var teamService = new TeamService();
            Teams = await teamService.GetTeams();

            //foreach (var team in Teams)
            //{
            //    if (team.Circuito.Equals("N"))
            //        TeamsNorte.Add(team);
            //    else
            //        TeamsSuroeste.Add(team);
            //}

            IsBusy = false;
        }

        public async void GetTeamInformation(int idTeam)
        {
            IsBusy = true;
            TeamInformation = new List<TeamInformation>();
           
            var teamService = new TeamService();
            TeamInformation = await teamService.GetTeamInformation(idTeam);

            IsBusy = false;
        }

        public async void GetPublicidad()
        {
            List<Publicidad> Publicidad = new List<Publicidad>();

            var publicidadService = new PublicidadService();
            Publicidad = await publicidadService.GetPublicidad();

            foreach (var p in Publicidad)
            {
                PublicidadHTML = $"<html><body  style='text-align: center;'>" +
                    $"<img src='http://www.deportes56.com/Imagenes/Publicidad/{p.RutaPublicidad}' style='width:100%; height: 60px; border:1px solid gray'>" +
                    "</body></html>";
            }
        }
    }
}
