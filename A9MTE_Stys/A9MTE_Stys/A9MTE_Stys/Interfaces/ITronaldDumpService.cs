using A9MTE_Stys.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace A9MTE_Stys.Interfaces
{
    public interface ITronaldDumpService
    {
        Task<TrumpQuote> GetQuoteAsync(string quoteUrl);
        Task<ImageSource> GetMemeAsync(string memeUrl);
    }
}
