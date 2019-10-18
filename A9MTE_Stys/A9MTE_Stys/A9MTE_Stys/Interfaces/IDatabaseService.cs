using A9MTE_Stys.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace A9MTE_Stys.Interfaces
{
    public interface IDatabaseService
    {
        Task<bool> AddJoke(JokeItem joke);
        List<JokeItem> GetJokes();
        Task<bool> DeleteJoke(JokeItem joke);

        Task<bool> AddQuote(QuoteItem quote);
        List<QuoteItem> GetQuotes();
        Task<bool> DeleteQuote(QuoteItem quote);

        bool DeleteDatabase();
    }
}
