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
	public partial class PlayerPage : ContentPage
	{
		public PlayerPage ()
		{
			InitializeComponent ();
            UpdateViewModel("Activos");
        }

        void UpdateViewModel(string tipo)
        {
            PlayersVM playersVM = new PlayersVM(tipo);

            BindingContext = playersVM;

            jugadoresListView.Header = tipo;
        }

        public async void OnFilterClicked(object sender, EventArgs args)
        {
            var action = await DisplayActionSheet("Filtrar", "Cancel", null, "Todos", "Activos", "Inactivos");
            //TeamFiltered(action);
            UpdateViewModel(action);
        }

    }
}