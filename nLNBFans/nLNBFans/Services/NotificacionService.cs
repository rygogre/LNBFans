using nLNBFans.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace nLNBFans.Services
{
    public class NotificacionService
    {
        public NotificacionService()
        {

        }

        public async void OnRegistered(string registrationId)
        {
            ///CheckFile();

            HttpClient client = new HttpClient();
                        
            var request = JsonConvert.SerializeObject("0");
            var body = new StringContent(request, System.Text.Encoding.UTF8, "application/json");
            client.BaseAddress = new Uri("http://lnbservices.azurewebsites.net");
            var url = "/api/RegisterNotification";

            var response = await client.PostAsync(url, body);

            if (response.IsSuccessStatusCode)
            {
                var estado = JsonConvert.DeserializeObject(response.Content.ReadAsStringAsync().Result).ToString();
                if (!string.IsNullOrEmpty(estado))
                {
                    var device = new DeviceUpdate
                    {
                        Handler = registrationId,
                        ID = estado,
                        Platform = "gcm",
                        Tags = new string[] { "juegos", "noticias" }
                    };
                    var deviceResquest = JsonConvert.SerializeObject(device);
                    body = new StringContent(deviceResquest, System.Text.Encoding.UTF8, "application/json");

                    response = await client.PutAsync(url, body);
                    var data = response.Content;
                    if (response.IsSuccessStatusCode)
                    {
                        var data1 = response.Content;
                    }
                }
            }
            else
            {
                string n = response.StatusCode.ToString();
            }
        }

        //protected async void CheckFile()
        //{
        //    PCLStorage.IFolder folder = PCLStorage.FileSystem.Current.LocalStorage;
        //     bool file = await Helpers.SettingHelper.IsFileExistAsync("tLNBFans.txt");
        //    if (!file)
        //       await Helpers.SettingHelper.CreateFile("tLNBFans.txt");
        //}
    }
}
