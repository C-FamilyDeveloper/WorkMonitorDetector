using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            return await httpClient.PostAsync("https://localhost:7261/api/sites",
                new StringContent(JsonSerializer.Serialize(acceptedSite), Encoding.UTF8, "application/json"));
        }
        public async Task<HttpResponseMessage> SendAsync(AcceptedApp acceptedApp)
        {
            using HttpClient httpClient = new();
            return await httpClient.PostAsync("https://localhost:7261/api/apps",
                new StringContent(JsonSerializer.Serialize(acceptedApp), Encoding.UTF8, "application/json"));
        }
    }
}
