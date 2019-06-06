using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace nLNBFans.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SchedulePage : ContentPage
	{
        int idTeam = 0;
        string actionSelect = "Todos";

        public SchedulePage ()
		{
			InitializeComponent ();

            var temporadaActual = Preferences.Get("temporadaActual", string.Empty);
            var etapa = Preferences.Get("etapa", string.Empty);

            DisplayAlert("Alert", etapa + temporadaActual, "Ok");
            
            int index = 0;
            if (etapa.Equals("SR"))
                index = 0;
            else
                if (etapa.Equals("SF"))
                index = 1;
            else
                if (etapa.Equals("FS"))
                index = 2;

            etapaPicker.SelectedIndex = index;
            yearPicker.SelectedIndex = 0;

            etapaPicker.SelectedIndexChanged += YearPicker_SelectedIndexChanged;
            yearPicker.SelectedIndexChanged += YearPicker_SelectedIndexChanged;

            UpdateViewModel(idTeam, temporadaActual, etapa);
        }

        void UpdateViewModel(int idTeam, string year, string etapa)
        {
            calendarioListView.Header = actionSelect;
            ViewModels.ScheduleVM scheduleVM = new ViewModels.ScheduleVM(idTeam, year, etapa);

            BindingContext = scheduleVM;
        }

        private void YearPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateViewModel(idTeam, yearPicker.SelectedItem.ToString(), EtapaShort());
        }

        public async void OnNewClicked(object sender, EventArgs args)
        {
            var action = await DisplayActionSheet("Filtrar", "Cancel", null, "Todos", "Indios", "Metros", "Huracanes", "Reales", "Leones", "Titanes", "Cañeros", "Soles");
            if (!action.Equals("Cancel"))
            {
                actionSelect = action.ToUpper();
                idTeam = action.Equals("Todos") ? 0 : int.Parse(Helpers.SettingHelper.TeamFiltered(action));
                UpdateViewModel(idTeam, yearPicker.SelectedItem.ToString(), EtapaShort());
            }
        }

        string EtapaShort()
        {
            string etapa = etapaPicker.SelectedItem.ToString();
            string etapaShort = "SR";

            if (etapa.Equals("S. Regular"))
                etapaShort = "SR";
            else
                if (etapa.Equals("SemiFinal"))
                etapaShort = "SF";
            else
                if (etapa.Equals("S. Final"))
                etapaShort = "FS";

            return etapaShort;
        }

        void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {

        }
    }
}