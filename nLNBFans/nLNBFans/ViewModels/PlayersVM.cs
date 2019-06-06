using nLNBFans.Models;
using nLNBFans.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace nLNBFans.ViewModels
{
    public class PlayersVM : INotifyPropertyChanged
    {
        private bool isBusy;
        private string filter;
        private List<Players> players;
        private string publicidadHTML;
        //private string title= "T";


        //public string Title
        //{
        //    get { return title; }
        //    set { title = value; OnPropertyChanged(); }
        //}

        public List<Players> Players
        {
            get { return players; }
            set { players = value; OnPropertyChanged(); }
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

        public string Filter
        {
            get
            {
                return filter;
            }
            set
            {
                if (filter != value)
                {
                    filter = value;
                    Search();
                    PropertyChanged?.Invoke(
                        this,
                        new PropertyChangedEventArgs(nameof(Filter)));
                }
            }
        }


        public PlayersVM(string tipo)
        {
            GetPlayers(tipo);
            GetPublicidad();
            
        }

        public PlayersVM(int idTeam)
        {
            GetPlayersTeam(idTeam);
        }



        public async void GetPlayers(string tipo)
        {
            IsBusy = true;
            Players = new List<Players>();

            var playersService = new PlayerService();
            Players = await playersService.GetPlayers();

            if (!tipo.Equals("Todos"))
                Players = Players.Where(p => tipo == "Activos" ? !p.IDEquipo.Equals("0") : p.IDEquipo.Equals("0")).ToList();

            IsBusy = false;
        }

        public async void GetPlayersTeam(int idTeam)
        {
            IsBusy = true;
            Players = new List<Players>();

            var playersService = new PlayerService();
            Players = await playersService.GetPlayersTeam(idTeam);
           

            //if (PlayersList.Count > 0)
            //{
            //    var query = from playerList in PlayersList
            //            where playerList.IDEquipo.Equals(idTeam.ToString())
            //            select playerList;

            //    Players = query.ToList();
            //}

            //foreach(var player in PlayersList)
            //{
            //    if (player.IDEquipo.Equals(idTeam.ToString()))
            //        Players.Add(player);
            //}

            IsBusy =  false;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public ICommand SearchCommand
        {
            get
            {
                return new Command(Search);
            }
        }

        void Search()
        {
            IsBusy = true;

            if (string.IsNullOrEmpty(Filter))
            {
                GetPlayers("Activos");
                //Players = new List<Players>(
                //    players.OrderBy(p => p.NombreCompleto));
            }
            else
            {
                Players = new List<Players>(players
                    .Where(p => p.NombreCompleto.ToLower().Contains(Filter.ToLower()))
                    .OrderBy(p => p.NombreCompleto));
            }

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
