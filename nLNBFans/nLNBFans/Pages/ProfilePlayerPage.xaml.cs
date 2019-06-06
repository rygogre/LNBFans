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
	public partial class ProfilePlayerPage : ContentPage
	{
		public ProfilePlayerPage (object player, int idPlayer)
		{
			InitializeComponent ();

            ViewModels.ProfilePlayerVM profilePlayerVM = new ViewModels.ProfilePlayerVM(player, idPlayer);

            BindingContext = profilePlayerVM;
        }
	}
}