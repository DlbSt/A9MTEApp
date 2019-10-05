using A9MTE_Stys.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace A9MTE_Stys.Services
{
    public class SettingsService : ISettingsService
    {
        public async Task<string> LoadSettings(string name)
        {
            return await SecureStorage.GetAsync(name);
        }

        public async Task<bool> SaveSettings(string name, string value)
        {
            try
            {
                await SecureStorage.SetAsync(name, value);
                return true;
            }
            catch { return false; }
        }
    }
}
