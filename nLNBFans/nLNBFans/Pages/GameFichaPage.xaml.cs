using nLNBFans.Interfaces;
using nLNBFans.Models;
using nLNBFans.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace nLNBFans.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class GameFichaPage : ContentPage
	{
        GameDetailsVM gameDetailsVM;
        object g = null;
        GameDay gameDay;

        public GameFichaPage (object game)
		{
			InitializeComponent ();
            g = game;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            gameDetailsVM = new GameDetailsVM(g);
            BindingContext = gameDetailsVM;
        }

        public void OnShareClicked(object sender, EventArgs args)
        {
            gameDay = g as GameDay;

            string statusGame = gameDay.ScoreCasa > 0 ? "FINAL" : gameDay.Hora;
            string score = statusGame == "FINAL" ? $"{gameDay.Visita} {gameDay.ScoreVisita} @ {gameDay.Casa} {gameDay.ScoreCasa}" :
                                                   $"{gameDay.Visita} vs {gameDay.Casa} {gameDay.NombreInstalacion}";

            //DependencyService.Get<IShareSocial>().ShareSocial("Compartir", score + " " + statusGame + " #LNBFans " + "#playStore goo.gl/mYKel4  - #appStore goo.gl/KtnF09", "http://deportes56.com/Imagenes/LNB/Detalles/LNB300.png", "LNB300");

            Helpers.SettingHelper.ShareUri(score + " " + statusGame  + " #LNBFans playStore goo.gl/mYKel4  - appStore goo.gl/KtnF09", "Compartir");
        }
    }
}