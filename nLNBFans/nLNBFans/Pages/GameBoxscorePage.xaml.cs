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
	public partial class GameBoxscorePage : ContentPage
	{
        object g = null;
        public GameBoxscorePage (object game)
		{
			InitializeComponent ();
            g = game;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            BoxScoreVM boxScoreVM = new BoxScoreVM(g);

            BindingContext = boxScoreVM;

        }
    }
}