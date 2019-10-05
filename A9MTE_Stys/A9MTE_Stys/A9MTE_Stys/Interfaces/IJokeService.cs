using A9MTE_Stys.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace A9MTE_Stys.Interfaces
{
    public interface IJokeService
    {
        Task<Joke> GetJokeAsync(string url, string category);
    }
}
