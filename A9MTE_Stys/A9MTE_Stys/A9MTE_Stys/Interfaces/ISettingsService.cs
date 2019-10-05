using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace A9MTE_Stys.Interfaces
{
    public interface ISettingsService
    {
        Task<string> LoadSettings(string name);
        Task<bool> SaveSettings(string name, string value);
    }
}
