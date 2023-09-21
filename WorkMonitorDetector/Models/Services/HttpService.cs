using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using WorkMonitorTypes.Requests;

namespace WorkMonitorDetector.Models.Services
{
    public class HttpService
    {
        private HttpClient httpClient;
        public HttpService()
        {
            httpClient = new();
        }
        public async Task<List<string>?> GetClientNamesAsync()
        {
            return await httpClient.GetFromJsonAsync<List<string>?>("https://localhost:7261/api/clients");
        }
        public async Task<HttpResponseMessage> SendAsync(AcceptedSite acceptedSite)
        {
            return await httpClient.PostAsJsonAsync("https://localhost:7261/api/sites", acceptedSite);
        }
        public async Task<HttpResponseMessage> SendAsync(AcceptedApp acceptedApp)
        {
            return await httpClient.PostAsJsonAsync("https://localhost:7261/api/apps", acceptedApp);
        }
    }
}
