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
    public class GettingService
    {
        public async Task<List<string>?> GetClientNamesAsync()
        {
            using HttpClient httpClient = new();
            var result = await httpClient.GetAsync("https://localhost:7261/api/apps");
            if (!result.IsSuccessStatusCode) { throw new Exception(); }
            return await result.Content.ReadFromJsonAsync<List<string>>();
        }
    }
}
