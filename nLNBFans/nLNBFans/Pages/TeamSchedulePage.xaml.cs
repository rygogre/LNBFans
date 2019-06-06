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
	public partial class TeamSchedulePage : ContentPage
	{
		public TeamSchedulePage (int idTeam)
		{
			InitializeComponent ();

            ScheduleVM scheduleVM = new ScheduleVM(idTeam);
            BindingContext = scheduleVM;
        }
	}
}