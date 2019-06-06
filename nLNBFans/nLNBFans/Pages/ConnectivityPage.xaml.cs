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
	public partial class ConnectivityPage : ContentPage
	{
		public ConnectivityPage ()
		{
			InitializeComponent ();
		}

        public void OnTapped(object sender, EventArgs e)
        {
            //if (Helpers.SettingHelper.ConnectionDevice())
            //    App.Navigator.PopToRootAsync();

            //TODO add code here
        }
    }
}