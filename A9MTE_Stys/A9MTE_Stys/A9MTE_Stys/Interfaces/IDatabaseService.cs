using A9MTE_Stys.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace A9MTE_Stys.Interfaces
{
    public interface IDatabaseService
    {
        bool AddJoke(JokeItem joke);
        List<JokeItem> GetJokes();
        bool DeleteJoke(JokeItem joke);
    }
}
