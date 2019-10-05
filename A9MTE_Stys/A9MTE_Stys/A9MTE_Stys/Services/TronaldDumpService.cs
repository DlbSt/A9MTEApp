using A9MTE_Stys.Interfaces;
using A9MTE_Stys.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace A9MTE_Stys.Services
{
    public class TronaldDumpService : ITronaldDumpService
    {
        private string jokeUrl = "https://api.tronalddump.io/random/quote";
        private string memeUrl = "https://api.tronalddump.io/random/meme";

        public async Task<TrumpQuote> GetJokeAsync()
        {
            HttpClient httpClient = new HttpClient();
            var uri = new Uri(string.Format(jokeUrl, string.Empty));
            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<TrumpQuote>(message);
            }
            else
            {
                return null;
            }
        }

        public async Task<ImageSource> GetMemeAsync()
        {
            HttpClient httpClient = new HttpClient();
            var uri = new Uri(string.Format(memeUrl, string.Empty));
            var response = await httpClient.GetAsync(uri);

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsByteArrayAsync();
                return ByteArrayToImage(message);
            }
            else
            {
                return null;
            }
        }
        private ImageSource ByteArrayToImage(byte[] imageBytes)
        {
            return ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }
    }
}
