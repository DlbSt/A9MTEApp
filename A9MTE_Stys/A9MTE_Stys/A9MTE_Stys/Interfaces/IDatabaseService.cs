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
        JokeItem GetRandomJoke();
        Task<bool> DeleteJoke(JokeItem joke);

        Task<bool> AddQuote(QuoteDbItem quote);
        List<QuoteDbItem> GetQuotes();
        QuoteDbItem GetRandomQuote();
        Task<bool> DeleteQuote(QuoteDbItem quote);

        Task<bool> AddMeme(MemeDbItem meme);
        List<MemeDbItem> GetMemes();
        MemeDbItem GetRandomMeme();
        Task<bool> DeleteMeme(MemeDbItem meme);

        bool DeleteDatabase();
    }
}
