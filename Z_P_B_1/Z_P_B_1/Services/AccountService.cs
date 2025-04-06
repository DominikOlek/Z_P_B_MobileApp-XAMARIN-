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
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;
using Z_P_B_1.Models;
using Z_P_B_1.Views;

namespace Z_P_B_1.Services
{
    public interface IAccountService
    {
        Task<bool> Reg(string info);
        Task<bool> Log(string info);
        Task<bool> RefSession();
        Task<bool> Out();
        Task<Info> GetInfo(bool all);
        Task<bool> SendConf();
        Task<bool> SendConfAnon(string email);
        Task<bool> ChangeInfo(string data);
        Task<bool> ChangePass(string data);
    }

    public class AccountService : IAccountService
    {
        private HttpClient httpClient = App.HttpClient;
        public CancellationTokenSource ctS = new CancellationTokenSource();
        public Database database = App.DataBase;


        public static Task WhenCanceled(CancellationToken cancellationToken)
        {
            var tcs = new TaskCompletionSource<bool>();
            cancellationToken.Register(s => ((TaskCompletionSource<bool>)s).SetResult(true), tcs);
            return tcs.Task;
        }

        public async Task<bool> Reg(string info)
        {
            ctS = new CancellationTokenSource();
            if (CrossConnectivity.Current.IsConnected && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string uri = "app/account/register";
                ctS.CancelAfter(8000);
                try
                {
                    using (var dnsTask = Dns.GetHostAddressesAsync(httpClient.BaseAddress.Host))
                        _ = await Task.WhenAny(WhenCanceled(ctS.Token), dnsTask);
                    ctS.Token.ThrowIfCancellationRequested();
                    HttpResponseMessage response = await httpClient.PostAsync(uri, new StringContent(info,Encoding.UTF8,"application/json"), ctS.Token);
                    if (!response.IsSuccessStatusCode)
                    {
                        return await Task.FromResult(false);
                    }
                }
                catch(Exception e) { return await Task.FromResult(false); }
            }
            return true;
        }

        public async Task<bool> Log(string info)
        {
            ctS = new CancellationTokenSource();
            if (CrossConnectivity.Current.IsConnected && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string uri = "app/account/login";
                ctS.CancelAfter(6000);
                try
                {
                    using (var dnsTask = Dns.GetHostAddressesAsync(httpClient.BaseAddress.Host))
                        _ = await Task.WhenAny(WhenCanceled(ctS.Token), dnsTask);
                    ctS.Token.ThrowIfCancellationRequested();
                    HttpResponseMessage response = await httpClient.PostAsync(uri, new StringContent(info, Encoding.UTF8, "application/json"), ctS.Token);
                    if (!response.IsSuccessStatusCode)
                    {
                        return await Task.FromResult(false);
                    }
                    else
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        if (content.Count() > 0)
                        {
                            string b = content.Remove(0,1);
                            var a = string.Concat("{ \"id\": 1 ," , b);
                            var list = JsonSerializer.Deserialize<SessionData>(a);
                            try
                            {
                                await database.SetTokens(list);
                            }
                            catch { return await Task.FromResult(false); }
                            return await Task.FromResult(true);
                        }
                    }
                }
                catch { return await Task.FromResult(false); }
            }
            return await Task.FromResult(true);
        }

        public async Task<bool> RefSession()
        {
            ctS = new CancellationTokenSource();
            if (CrossConnectivity.Current.IsConnected && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string uri = "app/account/refresh";
                ctS.CancelAfter(5000);
                try
                {
                    using (var dnsTask = Dns.GetHostAddressesAsync(httpClient.BaseAddress.Host))
                        _ = await Task.WhenAny(WhenCanceled(ctS.Token), dnsTask);
                    ctS.Token.ThrowIfCancellationRequested();
                    HttpResponseMessage response = await httpClient.PostAsync(uri, new StringContent(JsonSerializer.Serialize<SessionData>(await database.GetTokens()), Encoding.UTF8, "application/json"), ctS.Token);
                    if (!response.IsSuccessStatusCode)
                    {
                        return await Task.FromResult(false);
                    }
                    else
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        if (content.Count() > 0)
                        {
                            var list = JsonSerializer.Deserialize<SessionData>(content);
                            list.id = 1;
                            try
                            {
                                await database.SetTokens(list);
                                return await Task.FromResult(true);
                            }
                            catch { }
                            return await Task.FromResult(false);
                        }
                    }
                }
                catch (Exception e) { return await Task.FromResult(false); }
            }
            return await Task.FromResult(false);
        }

        public async Task<bool> Out()
        {
            ctS = new CancellationTokenSource();
            if (CrossConnectivity.Current.IsConnected && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string uri = "app/account/deleteRefresh";
                using (var ctS = new CancellationTokenSource(5000))
                {
                    try
                    {
                        using (var dnsTask = Dns.GetHostAddressesAsync(httpClient.BaseAddress.Host))
                            _ = await Task.WhenAny(WhenCanceled(ctS.Token), dnsTask);
                        ctS.Token.ThrowIfCancellationRequested();
                        var datab = await database.GetJWT();
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", datab);
                        using (HttpResponseMessage response = await httpClient.DeleteAsync(uri, ctS.Token))
                        {
                            response.EnsureSuccessStatusCode();
                            if (response.StatusCode == HttpStatusCode.Unauthorized)
                            {
                                if (await RefSession())
                                    return await Out();
                                else
                                {
                                    return await Task.FromResult(true);
                                }
                            }
                            else if (!response.IsSuccessStatusCode)
                            {
                                return await Task.FromResult(false);
                            }
                        }
                    }
                    catch (OperationCanceledException){ return await Task.FromResult(true); }
                    catch { return await Task.FromResult(false); }
                }
            }
            return true;
        }

        public async Task<Info> GetInfo(bool all)
        {
            var datab = await database.GetJWT();
            if (datab == null)
                return null;
            ctS = new CancellationTokenSource();
            if (CrossConnectivity.Current.IsConnected && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string uri = "app/account/userinfo/" + all;
                ctS.CancelAfter(5000);
                try
                {
                    using (var dnsTask = Dns.GetHostAddressesAsync(httpClient.BaseAddress.Host))
                        _ = await Task.WhenAny(WhenCanceled(ctS.Token), dnsTask);
                    ctS.Token.ThrowIfCancellationRequested();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", datab);
                    HttpResponseMessage response = await httpClient.GetAsync(uri, ctS.Token);

                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        if (await RefSession())
                            return await GetInfo(all);
                        else
                        {
                            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                        }
                    }else if (!response.IsSuccessStatusCode)
                    {
                        return null;
                    }
                    else
                    {
                        string content = await response.Content.ReadAsStringAsync();
                        if (content.Count() > 0)
                        {
                            var list = JsonSerializer.Deserialize<Info>(content);
                            return await Task.FromResult(list);
                        }
                    }
                }
                catch (Exception e) { return null; }
            }
            return null;
        }


        public async Task<bool> SendConf()
        {
            ctS = new CancellationTokenSource();
            if (CrossConnectivity.Current.IsConnected && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string uri = "app/account/confirmaction";
                ctS.CancelAfter(5000);
                try
                {
                    using (var dnsTask = Dns.GetHostAddressesAsync(httpClient.BaseAddress.Host))
                        _ = await Task.WhenAny(WhenCanceled(ctS.Token), dnsTask);
                    ctS.Token.ThrowIfCancellationRequested();
                    var datab = await database.GetJWT();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", datab);
                    HttpResponseMessage response = await httpClient.GetAsync(uri, ctS.Token);
                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception e) { return false; }
            }
            return false;
        }

        public async Task<bool> SendConfAnon(string email)
        {
            ctS = new CancellationTokenSource();
            if (CrossConnectivity.Current.IsConnected && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string uri = "app/account/confirmactionAnnon";
                ctS.CancelAfter(5000);
                try
                {
                    using (var dnsTask = Dns.GetHostAddressesAsync(httpClient.BaseAddress.Host))
                        _ = await Task.WhenAny(WhenCanceled(ctS.Token), dnsTask);
                    ctS.Token.ThrowIfCancellationRequested();
                    HttpResponseMessage response = await httpClient.PostAsync(uri, new StringContent(JsonSerializer.Serialize(email), Encoding.UTF8, "application/json"), ctS.Token);
                    if (!response.IsSuccessStatusCode)
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception e) { return false; }
            }
            return false;
        }

        public async Task<bool> ChangeInfo(string data)
        {
            ctS = new CancellationTokenSource();
            if (CrossConnectivity.Current.IsConnected && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string uri = "app/account/changedUser";
                ctS.CancelAfter(5000);
                try
                {
                    using (var dnsTask = Dns.GetHostAddressesAsync(httpClient.BaseAddress.Host))
                        _ = await Task.WhenAny(WhenCanceled(ctS.Token), dnsTask);
                    ctS.Token.ThrowIfCancellationRequested();
                    var datab = await database.GetJWT();
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", datab);
                    HttpResponseMessage response = await httpClient.PostAsync(uri, new StringContent(data, Encoding.UTF8, "application/json"), ctS.Token);
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        if (await RefSession())
                            return await ChangeInfo(data);
                        else
                        {
                            Application.Current.MainPage = new LoginPage();
                            await Shell.Current.GoToAsync($"//{nameof(LoginPage)}");
                        }
                    }
                    else if (!response.IsSuccessStatusCode)
                    {
                        return await Task.FromResult(false);
                    }
                }
                catch (Exception e) { return await Task.FromResult(false); }
            }
            return true;
        }

        public async Task<bool> ChangePass(string data)
        {
            ctS = new CancellationTokenSource();
            if (CrossConnectivity.Current.IsConnected && Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                string uri = "app/account/changePassword";
                ctS.CancelAfter(5000);
                try
                {
                    using (var dnsTask = Dns.GetHostAddressesAsync(httpClient.BaseAddress.Host))
                        _ = await Task.WhenAny(WhenCanceled(ctS.Token), dnsTask);
                    ctS.Token.ThrowIfCancellationRequested();
                    HttpResponseMessage response = await httpClient.PostAsync(uri, new StringContent(data, Encoding.UTF8, "application/json"), ctS.Token);
                     if (!response.IsSuccessStatusCode)
                    {
                        return await Task.FromResult(false);
                    }
                }
                catch (Exception e) { return await Task.FromResult(false); }
            }
            return await Task.FromResult(true);
        }
    }
}