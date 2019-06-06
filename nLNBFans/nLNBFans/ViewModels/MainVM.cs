using nLNBFans.Models;
using nLNBFans.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace nLNBFans.ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {
        private bool isBusy;
        private bool isBusyStanding;
        private int rowGameDay;
        DateTime fecha = DateTime.Now;
        List<GameDay> gamesDay;
        List<Standing> standing;
        private Color hasBordeVisita;
        private Color hasBordeCasa;
        private string publicidadHTML;

        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value; OnPropertyChanged(); }
        }

        public bool IsBusyStanding
        {
            get { return isBusyStanding; }
            set { isBusyStanding = value; OnPropertyChanged(); }
        }

        public ObservableCollection<MenuItems> Menu { get; set; }
        public List<GameDay> GamesDay
        {
            get { return gamesDay; }
            set { gamesDay = value; OnPropertyChanged("GamesDay"); }
        }
        public List<Standing> Standing
        {
            get { return standing; }
            set { standing = value; OnPropertyChanged(); }
        }
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; OnPropertyChanged(); }
        }

        public int RowGameDay
        {
            get { return rowGameDay; }
            set { rowGameDay = value; OnPropertyChanged(); }
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

        public string PublicidadHTML
        {
            get { return publicidadHTML; }
            set { publicidadHTML = value; OnPropertyChanged(); }
        }

        public Command DateCommand
        {
            get {
                return new Command((e) => {
                    Fecha = Fecha.AddDays(int.Parse(e.ToString()));

                    GetGameDay(fecha.ToString("yyyyMMdd"));
                });
            }
        }

        public Command SelectDateCommand
        {
            get
            {
                return new Command(() =>
                {
                    GetGameDay(Fecha.ToString("yyyyMMdd"));
                });
            }
        }

        public Command CircuitoCommand
        {
            get
            {
                return new Command((e) => {                   
                    GetStanding(e.ToString());
                });
            }
        }

        public Command RefreshCommand
        {
            get
            {
                return new Command(() => {
                    GetGameDay(null);
                    GetStanding("N");
                });
            }
        }


        public MainVM()
        {
            LoadMenu();
            GetStanding("N");
            GetGameDay(null);
            GetPublicidad();
        }

        public MainVM(string menu)
        {
            LoadMenu();           
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region METODOS
        #region Menu
        /// <summary>
        /// Generar menu.
        /// </summary>
        private void LoadMenu()
        {
            Menu = new ObservableCollection<MenuItems>();

            Menu.Add(new MenuItems {
                Icon = "home.png",
                Title = "Inicio",
                TypeTarget = "MainPage"   
            });

            Menu.Add(new MenuItems
            {
                Icon = "newspaper.png",
                Title = "Noticias",
                TypeTarget = "NewsPage"
            });

            Menu.Add(new MenuItems
            {
                Icon = "ball.png",
                Title = "Equipos",
                TypeTarget = "TeamPage"
            });

            Menu.Add(new MenuItems
            {
                Icon = "playermenu.png",
                Title = "Jugadores",
                TypeTarget = "PlayerPage"
            });

            Menu.Add(new MenuItems
            {
                Icon = "ball.png",
                Title = "Estadísticas",
                TypeTarget = "LeadersPage"
            });

            Menu.Add(new MenuItems
            {
                Icon = "calendario.png",
                Title = "Calendario",
                TypeTarget = "SchedulePage"
            });

            Menu.Add(new MenuItems
            {
                Icon = "ic_settings.png",
                Title = "Preferencias",
                TypeTarget = "PreferenciasPage"
            });
        }
        #endregion

        public async void GetGameDay(string fecha)
        {
            IsBusy = true;
            GamesDay = new List<GameDay>();
            RowGameDay = 1;
            
            var gameDayService = new GameDayService();
            GamesDay = await gameDayService.GetGameDay(fecha);

            if (GamesDay.Count > 0)
            {
                foreach (var items in GamesDay)
                {
                    //GamesDay.Add(items);
                    Fecha = items.Fecha;
                }
                RowGameDay = GamesDay.Count * 110;
            }

            IsBusy = false;        
        }

        public async void GetStanding(string circuito)
        {
            IsBusyStanding = true;
            Standing = new List<Standing>();

            var standingService = new StandingService();
            Standing = await standingService.GetStanding(circuito);

            if (circuito.Equals("N"))
            {
                HasBordeVisita = Color.FromHex("#01579b");
                HasBordeCasa = Color.Transparent;
            }
            else
            {
                HasBordeVisita = Color.Transparent;
                HasBordeCasa = Color.FromHex("#01579b");
            }

            IsBusyStanding = false;           
        }

        
        public async void GetPublicidad()
        {            
            List<Publicidad> Publicidad = new List<Publicidad>();

            var publicidadService = new PublicidadService();
            Publicidad = await publicidadService.GetPublicidad();

            foreach(var p in Publicidad)
            {
                PublicidadHTML = $"<html><body  style='text-align: center;'>" +
                    $"<img src='http://www.deportes56.com/Imagenes/Publicidad/{p.RutaPublicidad}' style='width:100%; height: 60px; border:1px solid gray'>" +
                    "</body></html>";
            }
        }

        //private void GoTo(string typeTarget)
        //{
        //    switch(typeTarget)
        //    {
        //        case "PlayersPage":
        //            App.Navigator.PushAsync(new PlayerPage());
        //            break;
        //        default:
        //            break;

        //    }
        //}
        #endregion


        //async Task<List<GameDay>> GetGame()
        //{
        //    List<GameDay> n = new List<Models.GameDay>();

        //    n.Add(new GameDay
        //    { IDalendario = 0, NombreInstalacion = "Test", ScoreCasa = 0, ScoreVisita = 0, Casa = "Indios", Visita = "Huracanas" });

        //    n.Add(new GameDay
        //    { IDalendario = 0, NombreInstalacion = "Test", ScoreCasa = 100, ScoreVisita = 90, Casa = "Metros", Visita = "Soles" }

        //        );

        //    return n;
        //}

    }
}
