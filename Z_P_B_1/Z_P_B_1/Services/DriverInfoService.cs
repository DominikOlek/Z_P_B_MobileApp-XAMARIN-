using Plugin.Connectivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Z_P_B_1.Models;
using Z_P_B_1.Services.Interfaces;
using Z_P_B_1.Views;

namespace Z_P_B_1.Services
{
    class DriverInfoService : IDataServiceOnlyNet<DriverInfoDto>
    {
        public HttpClient httpClient = App.HttpClient;
        public Database database = App.DataBase;
        public CancellationTokenSource ctS = new CancellationTokenSource();
        public IAccountService accountService => DependencyService.Get<IAccountService>();

        public static Task WhenCanceled(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
            return tcs.Task;
        }

        public async Task<DriverInfoDto> GetItems()
        {
            ctS = new CancellationTokenSource();
            if (CrossConnectivity.Current.IsConnected && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string uri = "app/account/mandats";
                ctS.CancelAfter(5000);
                try
                {
                    using (var dnsTask = Dns.GetHostAddressesAsync(httpClient.BaseAddress.Host))
                        _ = await Task.WhenAny(WhenCanceled(ctS.Token), dnsTask);
                    ctS.Token.ThrowIfCancellationRequested();
                    var datab = await database.GetJWT();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", datab);
                    HttpResponseMessage response = await httpClient.GetAsync(uri, ctS.Token);
                    if (response.IsSuccessStatusCode)
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        if (content.Count() > 0)
                        {
                            var list = JsonSerializer.Deserialize<DriverInfoDto>(content);
                            if (list.mandats == null) {
                                list.mandats = new MandatDto[] { new MandatDto { title = "Brak mandatów" } };
                            }
                            return await Task.FromResult(list);
                        }
                    }else if(response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        if (await accountService.RefSession())
                            return await GetItems();
                        else {
                            Application.Current.MainPage = new LoginPage();
                            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                        }
                    }
                    else
                    {
                        return new DriverInfoDto { mandats = new MandatDto[] { new MandatDto { title = "Brak mandatów" } } };
                    }
                }
                catch { }
            }
            return null;
        }
    }
}
