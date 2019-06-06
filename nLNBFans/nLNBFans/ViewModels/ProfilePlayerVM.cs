using nLNBFans.Models;
using nLNBFans.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace nLNBFans.ViewModels
{
    public class ProfilePlayerVM : INotifyPropertyChanged
    {
        private bool isBusy;
        private Players players;
        private List<PlayerStats> playerStats;
        private Color hasBordeSerieRegular;
        private Color hasBordeSemiFinal;
        private Color hasBordeSerieFinal;

        public Players Players
        {
            get { return players; }
            set { players = value; OnPropertyChanged(); }
        }

        public List<PlayerStats> PlayerStats
        {
            get { return playerStats; }
            set { playerStats = value; OnPropertyChanged(); }
        }

        public Color HasBordeSerieRegular
        {
            get { return hasBordeSerieRegular; }
            set { hasBordeSerieRegular = value; OnPropertyChanged(); }
        }

        public Color HasBordeSemiFinal
        {
            get { return hasBordeSemiFinal; }
            set { hasBordeSemiFinal = value; OnPropertyChanged(); }
        }

        public Color HasBordeSerieFinal
        {
            get { return hasBordeSerieFinal; }
            set { hasBordeSerieFinal = value; OnPropertyChanged(); }
        }


        public Command EtapaCommand
        {
            get
            {
                return new Command((e) => {
                    SetBorderEtapa(e.ToString());
                    GetPlayerStats(Players.IDPlayer, e.ToString());
                });
            }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        //Recibe un PLAYER.... o *IDPlayer
        public ProfilePlayerVM(object player, int idPlayer)
        {           
            if (player != null)            
                Players = (Players)player;
            else
                GetPlayer(idPlayer);

            GetPlayerStats(idPlayer, "SR");
            SetBorderEtapa("SR");
        }

        private async void GetPlayer(int idPlayer)
        {
            IsBusy = true;
            PlayerService playerService = new PlayerService();
            Players = await playerService.GetPlayerID(idPlayer);
            IsBusy = false;
        }

        private async void GetPlayerStats(int idPlayer, string etapa)
        {
            IsBusy = true;
            PlayerService playerService = new PlayerService();
            PlayerStats = await playerService.GetPlayerStats(idPlayer, etapa);
            IsBusy = false;
        }

        private void SetBorderEtapa(string etapa)
        {
            //IsBusy = true;
            HasBordeSerieRegular = Color.Transparent;
            HasBordeSemiFinal = Color.Transparent;
            HasBordeSerieFinal = Color.Transparent;           

            switch (etapa)
            {
                case "SR":
                    HasBordeSerieRegular = Color.FromHex("#01579b");
                    break;
                case "SF":
                    HasBordeSemiFinal = Color.FromHex("#01579b");
                    break;
                case "FS":
                    hasBordeSerieFinal = Color.FromHex("#01579b");
                    break;
                default:
                    break;
            }
            //IsBusy = false;
        }
    }
}
