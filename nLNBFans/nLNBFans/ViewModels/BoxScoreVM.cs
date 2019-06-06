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
    public class BoxScoreVM : INotifyPropertyChanged
    {
        private bool isBusy;
        private List<Boxscore> boxScore;
        private GameDay gameDay;
        private int idCalendario;
        private Color hasBordeVisita;
        private Color hasBordeCasa;

        public List<Boxscore> BoxScore
        {
            get { return boxScore; }
            set { boxScore = value; OnPropertyChanged(); }
        }

        public GameDay GameDay
        {
            get { return gameDay; }
            set { gameDay = value; OnPropertyChanged(); }
        }

        public Color HasBordeVisita
        {
            get { return hasBordeVisita; }
            set { hasBordeVisita = value; OnPropertyChanged(); }
        }

        public Color HasBordeCasa
        {
            get { return hasBordeCasa; }
            set { hasBordeCasa = value; OnPropertyChanged(); }
        }

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        public Command TeamBoxScoreVisitaCommand
        {
            get
            {
                return new Command((e) => {
                    HasBordeVisita = Color.FromHex("#01579b");
                    HasBordeCasa = Color.Transparent;
                    GetBoxScore((int)e);
                });
            }
        }

        public Command TeamBoxScoreCasaCommand
        {
            get
            {
                return new Command((e) => {
                    HasBordeVisita = Color.Transparent;
                    HasBordeCasa = Color.FromHex("#01579b");
                    GetBoxScore((int)e);
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public BoxScoreVM(object game)
        {
            var g = game as GameDay;

            GameDay = g;
            idCalendario = g.IDCalendario;

            GetBoxScore(g.IDEquipoVisita);

            HasBordeVisita = Color.FromHex("#01579b");
            HasBordeCasa = Color.Transparent;
        }

        public async void GetBoxScore(int idTeam)
        {
            IsBusy = true;
            BoxScore = new List<Boxscore>();

            var boxScoreService = new BoxScoreService();
            BoxScore = await boxScoreService.GetBoxScore(idCalendario, idTeam);

            IsBusy = false;
        }
    }
}
