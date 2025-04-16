using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Z_P_B_1.Services.Interfaces;
using Z_P_B_1.Models;
using Plugin.Connectivity;
using Xamarin.Essentials;
using System.Threading;
using System.Linq;
using System.Text.Json;
using System.Net;
using Z_P_B_1.Views;
using Xamarin.Forms;
using System.Net.Http.Headers;

namespace Z_P_B_1.Services
{
    public class TaryfikatorsService : IDataMainServices<Taryfikator>
    {
        public Database database = App.DataBase;
        public HttpClient httpClient = App.HttpClient;
        public CancellationTokenSource ctS = new CancellationTokenSource();
        List<Taryfikator> FromLocal;
        public IAccountService accountService => DependencyService.Get<IAccountService>();

        public static Task WhenCanceled(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
            return tcs.Task;
        }

        public async Task<IEnumerable<Taryfikator>> GetItems(bool mustRefresh)
        {
            ctS = new CancellationTokenSource();
            if (!mustRefresh)
            {
                FromLocal = await database.GetTar();
                if (FromLocal.Count > 0)
                    return FromLocal;
            }
            if (CrossConnectivity.Current.IsConnected && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string uri = "drogowe/GetTaryfikator";
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
                            var list = JsonSerializer.Deserialize<List<Taryfikator>>(content);
                            try
                            {
                                await database.ClearAllTar();
                                await database.AddTar(list);
                                FromLocal = list;
                            }
                            catch { }
                            return await Task.FromResult(list);
                        }
                    }
                }
                catch { }
            }
            ctS.Dispose();
            FromLocal = await database.GetTar();
            if (FromLocal.Count > 0)
            {
                return FromLocal;
            }
            return null;
        }
        public IEnumerable<Taryfikator> Search(string searchText)
        {
            try
            {
                return FromLocal.Where(a => a.title.ToLower().Contains(searchText.ToLower()) || a.pktNumber.ToString() == searchText);
            }
            catch { return null; }
        }

    }
}
