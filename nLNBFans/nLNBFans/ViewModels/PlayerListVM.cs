using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace nLNBFans.ViewModels
{
    public class PlayerListVM : INotifyPropertyChanged
    {
        public PlayerListVM()
        {

            SearchCommand = new Command(async (object obj) =>
            {
                using (var client = new HttpClient())
                {
                    SearchResult = await client.GetStringAsync("https://www.googleapis.com/customsearch/v1?key=AIzaSyD44XPaSG0I-jqOSXCWlQCOJtQ4WiN-c4o&cx=017576662512468239146:omuauf_lfve&q=" + obj);
                }
            });
        }
        
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public ICommand SearchCommand { get; }
        string searchResult;

        public string SearchResult
        {
            get
            {
                return searchResult;
            }

            set
            {
                searchResult = value;
                OnPropertyChanged();
            }
        }
    }
}
