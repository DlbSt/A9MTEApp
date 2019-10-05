using A9MTE_Stys.Interfaces;
using A9MTE_Stys.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace A9MTE_Stys.Services
{
    public class JokeService : IJokeService
    {
        public async Task<Joke> GetJokeAsync(string url, string category)
        {
            HttpClient httpClient = new HttpClient();
            var uri = new Uri(string.Format(url + category, string.Empty));
            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<Joke>(message);
            }
            else
            {
                return null;
            }
        }
    }
}
