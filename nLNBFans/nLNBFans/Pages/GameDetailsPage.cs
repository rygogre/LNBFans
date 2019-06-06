using nLNBFans.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace nLNBFans.Pages
{
	public class GameDetailsPage : TabbedPage
    {
		public GameDetailsPage (object game)
		{
            var g = game as GameDay;
            Title = $"{g.Visita} @ {g.Casa}";
            BarBackgroundColor = Color.FromHex("#01579b");

            Children.Add(new GameFichaPage(game));
            Children.Add(new GameBoxscorePage(game));
        }
	}
}