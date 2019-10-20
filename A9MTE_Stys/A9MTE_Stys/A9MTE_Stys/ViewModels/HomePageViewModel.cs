using A9MTE_Stys.Enums;
using A9MTE_Stys.Interfaces;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace A9MTE_Stys.ViewModels
{
    public class HomePageViewModel : BindableBase
    {
        #region Services
        private readonly ISettingsService _settingsService;
        #endregion

        #region Fields
        private string jokeUrl = "https://api.chucknorris.io/jokes/random?category=";
        private string memeUrl = "https://api.tronalddump.io/random/meme";
        private string quoteUrl = "https://api.tronalddump.io/random/quote";
        private int memeLimit = 5;
        #endregion

        public HomePageViewModel(ISettingsService settingsService)
        {
            _settingsService = settingsService;

            InitSettings();
        }

        public async void InitSettings()
        {
            var jokeUrl = await _settingsService.LoadSettings(SettingsEnum.JokeUrl.ToString());
            if (string.IsNullOrEmpty(jokeUrl)) await _settingsService.SaveSettings(SettingsEnum.JokeUrl.ToString(), this.jokeUrl);

            var memeUrl = await _settingsService.LoadSettings(SettingsEnum.MemeUrl.ToString());
            if (string.IsNullOrEmpty(memeUrl)) await _settingsService.SaveSettings(SettingsEnum.MemeUrl.ToString(), this.memeUrl);

            var quoteUrl = await _settingsService.LoadSettings(SettingsEnum.QuoteUrl.ToString());
            if (string.IsNullOrEmpty(quoteUrl)) await _settingsService.SaveSettings(SettingsEnum.QuoteUrl.ToString(), this.quoteUrl);

            var memeLimit = await _settingsService.LoadSettings(SettingsEnum.MemeLimit.ToString());
            if (string.IsNullOrEmpty(memeLimit)) await _settingsService.SaveSettings(SettingsEnum.MemeLimit.ToString(), this.memeLimit.ToString());
        }
    }
}
