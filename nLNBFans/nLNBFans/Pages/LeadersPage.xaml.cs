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
	public partial class LeadersPage : ContentPage
	{
        string year = string.Empty;
        string etapa = string.Empty;
        string equipoFiltro = "t";

        public LeadersPage ()
		{
			InitializeComponent ();

            categoriaPicker.SelectedIndex = 0;
            etapaPicker.SelectedIndex = 0;
            yearPicker.SelectedIndex = 0;

            categoriaPicker.SelectedIndexChanged += CategoriaPicker_SelectedIndexChanged;
            etapaPicker.SelectedIndexChanged += CategoriaPicker_SelectedIndexChanged;
            yearPicker.SelectedIndexChanged += CategoriaPicker_SelectedIndexChanged;

            UpdateViewModel();
        }

        private void CategoriaPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateViewModel();
        }

        void UpdateViewModel()
        {
            LeadersVM leadersVM = new LeadersVM(equipoFiltro,
                                                 categoriaPicker.SelectedItem.ToString(),
                                                 yearPicker.SelectedItem.ToString(),
                                                 etapaPicker.SelectedItem.ToString()
                                                 );

            BindingContext = leadersVM;
        }

        public async void OnNewClicked(object sender, EventArgs args)
        {
            var action = await DisplayActionSheet("Filtrar", "Cancel", null, "Todos", "Circuito Norte", "Indios", "Metros", "Huracanes", "Reales", "Circuito Suroeste", "Leones", "Titanes", "Cañeros", "Soles");
            if (!action.Equals("Cancel"))
            {
                equipoFiltro = Helpers.SettingHelper.TeamFiltered(action);
                UpdateViewModel();
            }
        }
    }
}