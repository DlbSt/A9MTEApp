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
        public async Task<TrumpQuote> GetQuoteAsync(string quoteUrl)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 0, 3);
            var uri = new Uri(string.Format(quoteUrl, string.Empty));

            HttpResponseMessage response = null;

            try
            {
                response = await httpClient.GetAsync(uri);
            }
            catch
            {
                return null;
            }

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

        public async Task<byte[]> GetMemeAsync(string memeUrl)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = new TimeSpan(0, 0, 1);
            var uri = new Uri(string.Format(memeUrl, string.Empty));

            HttpResponseMessage response = null;

            try
            {
                response = await httpClient.GetAsync(uri);
            }
            catch
            {
                return null;
            }

            if (response.IsSuccessStatusCode)
            {
                var message = await response.Content.ReadAsByteArrayAsync();
                return message;
            }
            else
            {
                return null;
            }
        }
    }
}
