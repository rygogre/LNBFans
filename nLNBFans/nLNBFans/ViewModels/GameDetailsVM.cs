using nLNBFans.Models;
using nLNBFans.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace nLNBFans.ViewModels
{
    public class GameDetailsVM : INotifyPropertyChanged
    {
        private bool isBusy;
        private GameDay game;
        private GameScoreTime gameScoreTime;        
        private GameTotals gameTotalsVisita;
        private GameTotals gameTotalsCasa;
        private string publicidadHTML;

        public GameDay Game
        {
            get { return game; }
            set { game = value; OnPropertyChanged(); }
        }

        public GameScoreTime GameScoreTime
        {
            get { return gameScoreTime; }
            set { gameScoreTime = value; OnPropertyChanged(); }
        }
               
        public GameTotals GameTotalsVisita
        {
            get { return gameTotalsVisita; }
            set { gameTotalsVisita = value; OnPropertyChanged(); }
        }

        public GameTotals GameTotalsCasa
        {
            get { return gameTotalsCasa; }
            set { gameTotalsCasa = value; OnPropertyChanged(); }
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
        
        public GameDetailsVM(object game)
        {
            var gameDay = game as GameDay;            
            GetGameDetail(gameDay);
            GetGameScoreTime(gameDay.IDCalendario);
            GetGameTotals(gameDay.IDCalendario, gameDay.IDEquipoVisita);
            GetPublicidad();
        }

        public void GetGameDetail(GameDay game)
        {            
            IsBusy = true;

            if (game != null)
            {
                Game = new GameDay();
                this.game = game;
            }

            IsBusy = false;

            //return Game;            
        }

        public async void GetGameScoreTime(int idCalendario)
        {
            IsBusy = true;
            GameScoreTime = new GameScoreTime();

            var gameDetailsService = new GameDetailsService();
            GameScoreTime = await gameDetailsService.GetGameScoreTime(idCalendario);
            
            IsBusy = false;
        }

        public async void GetGameTotals(int idCalendario, int idTeamVisita)
        {
            IsBusy = true;
            GameTotalsVisita = new GameTotals();
            GameTotalsCasa = new GameTotals();

            var gameDetailsService = new GameDetailsService();
            var totals = await gameDetailsService.GetGameTotals(idCalendario);

            foreach(var item in totals)
            {
                if (item.IDEquipo == idTeamVisita)
                    GameTotalsVisita = item;
                else
                    GameTotalsCasa = item;
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
