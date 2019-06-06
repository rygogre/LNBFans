using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace nLNBFans.Pages
{
	public class TeamDetailPage : TabbedPage
    {
		public TeamDetailPage (int idTeam, string teamName)
		{
            Title = teamName;
            BarBackgroundColor = Color.FromHex("#01579b");

            Children.Add(new TeamPlayerPage(idTeam));
            Children.Add(new TeamSchedulePage(idTeam));
            Children.Add(new TeamInformation(idTeam));
        }
	}
}