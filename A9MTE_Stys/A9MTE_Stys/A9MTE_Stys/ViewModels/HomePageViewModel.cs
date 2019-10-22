using A9MTE_Stys.Enums;
using A9MTE_Stys.Extensions;
using A9MTE_Stys.Interfaces;
using A9MTE_Stys.Model;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;

namespace A9MTE_Stys.ViewModels
{
    public class HomePageViewModel : BindableBase
    {
        #region Services
        private readonly ISettingsService _settingsService;
        private readonly IDatabaseService _databaseService;
        private readonly IToastMessage _toastMessage;
        #endregion

        #region Properties
        private JokeItem joke = new JokeItem
        {
            Id = 0,
            Icon = "chucknorris.png",
            Category = CategoryEnum.Animal,
            Joke = "No joke is available!"
        };
        public JokeItem Joke
        {
            get { return joke; }
            set { SetProperty(ref joke, value); }
        }

        private QuoteItem quote = new QuoteItem
        {
            Id = 0,
            Icon = "trumpface.png",
            Quote = "No quote was available!",
            Tags = new List<string> { string.Empty }
        };
        public QuoteItem Quote
        {
            get { return quote; }
            set { SetProperty(ref quote, value); }
        }
        #endregion

        #region Fields
        private string jokeUrl = "https://api.chucknorris.io/jokes/random?category=";
        private string memeUrl = "https://api.tronalddump.io/random/meme";
        private string quoteUrl = "https://api.tronalddump.io/random/quote";
        private const string notConnectedMessage = "Not connected to internet!";
        private int memeLimit = 5;
        #endregion

        public HomePageViewModel(ISettingsService settingsService,
                                 IDatabaseService databaseService,
                                 IToastMessage toastMessage)
        {
            _settingsService = settingsService;
            _databaseService = databaseService;
            _toastMessage = toastMessage;

            var dbJoke = _databaseService.GetRandomJoke();
            if (dbJoke != null) Joke = dbJoke;

            var dbQuote = _databaseService.GetRandomQuote();
            if (dbQuote != null) Quote = new QuoteItem
            {
                Tags = dbQuote.Tags.Split(';').ToList(),
                Icon = dbQuote.Icon,
                Id = dbQuote.Id,
                Quote = dbQuote.Quote
            };

            InitSettings();

            if (!ExtensionMethods.IsConnected()) _toastMessage.ShowToast(notConnectedMessage);
        }

        #region HelperMethods
        public async void InitSettings()
        {
            var jokeUrl = await _settingsService.LoadSettings(SettingsEnum.JokeUrl.ToString());
            if (string.IsNullOrEmpty(jokeUrl)) await _settingsService.SaveSettings(SettingsEnum.JokeUrl.ToString(), this.jokeUrl);

            var memeUrl = await _settingsService.LoadSettings(SettingsEnum.MemeUrl.ToString());
            if (string.IsNullOrEmpty(memeUrl)) await _settingsService.SaveSettings(SettingsEnum.MemeUrl.ToString(), this.memeUrl);

            var quoteUrl = await _settingsService.LoadSettings(SettingsEnum.QuoteUrl.ToString());
            if (string.IsNullOrEmpty(quoteUrl)) await _settingsService.SaveSettings(SettingsEnum.QuoteUrl.ToString(), this.quoteUrl);

            var memeLimit = await _settingsService.LoadSettings(SettingsEnum.MemeLimit.ToString());
            if (string.IsNullOrEmpty(memeLimit) || memeLimit == 0.ToString()) await _settingsService.SaveSettings(SettingsEnum.MemeLimit.ToString(), this.memeLimit.ToString());
        }
        #endregion
    }
}
