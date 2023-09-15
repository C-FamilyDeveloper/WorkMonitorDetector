using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WorkMonitorTypes.Requests;

namespace WorkMonitorDetector.Models.Services
{
    public class SendingService
    {
        public async Task<HttpResponseMessage> SendAsync(AcceptedSite acceptedSite)
        {
            using HttpClient httpClient = new();
            return await httpClient.PostAsJsonAsync("https://localhost:7261/api/sites", acceptedSite);
        }
        public async Task<HttpResponseMessage> SendAsync(AcceptedApp acceptedApp)
        {
            using HttpClient httpClient = new();
            return await httpClient.PostAsJsonAsync("https://localhost:7261/api/apps",acceptedApp);
        }
    }
}
