using A9MTE_Stys.Enums;
using A9MTE_Stys.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace A9MTE_Stys.ViewModels
{
    public class SettingsPageViewModel : BindableBase
    {
        private readonly IPageDialogService _dialogService;
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;

        public DelegateCommand<string> ShowJokeAddressPopUpCommand { get; set; }
        public DelegateCommand<string> ShowMemeAddressPopUpCommand { get; set; }
        public DelegateCommand<string> ShowQuoteAddressPopUpCommand { get; set; }
        public SettingsPageViewModel(INavigationService navigationService,
                                     IPageDialogService dialogService,
                                     ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _dialogService = dialogService;
            _settingsService = settingsService;

            ShowJokeAddressPopUpCommand = new DelegateCommand<string>(ShowJokeAddressPopUpWindowAsync);
            ShowMemeAddressPopUpCommand = new DelegateCommand<string>(ShowMemeAddressPopUpWindowAsync);
            ShowQuoteAddressPopUpCommand = new DelegateCommand<string>(ShowQuoteAddressPopUpWindowAsync);
            
            LoadSettings();
        }

        private async void ShowJokeAddressPopUpWindowAsync(string param)
        {
            await _navigationService.NavigateAsync("TextPickerPage", new NavigationParameters
            {
                { "type", PopUpTypeEnum.ChuckJokes.ToString() },
                { "url", param }
            });
        }

        private async void ShowMemeAddressPopUpWindowAsync(string param)
        {
            await _navigationService.NavigateAsync("TextPickerPage", new NavigationParameters
            {
                { "type", PopUpTypeEnum.TrumpMemes.ToString() },
                { "url", param }
            });
        }

        private async void ShowQuoteAddressPopUpWindowAsync(string param)
        {
            await _navigationService.NavigateAsync("TextPickerPage", new NavigationParameters
            {
                { "type", PopUpTypeEnum.TrumpQuotes.ToString() },
                { "url", param }
            });
        }

        private string jokeUrl = "https://api.chucknorris.io/jokes/random?category=";
        public string JokeUrl
        {
            get { return jokeUrl; }
            set
            {
                SetProperty(ref jokeUrl, value);
                SaveSettings(SettingsEnum.JokeUrl.ToString(), value);
            }
        }

        private string memeUrl = "https://api.tronalddump.io/random/meme";
        public string MemeUrl
        {
            get { return memeUrl; }
            set
            {
                SetProperty(ref memeUrl, value);
                SaveSettings(SettingsEnum.MemeUrl.ToString(), value);
            }
        }

        private string quoteUrl = "https://api.tronalddump.io/random/quote";
        public string QuoteUrl
        {
            get { return quoteUrl; }
            set
            {
                SetProperty(ref quoteUrl, value);
                SaveSettings(SettingsEnum.QuoteUrl.ToString(), value);
            }
        }

        private async void LoadSettings()
        {
            try
            {
                var jokeString = await _settingsService.LoadSettings(SettingsEnum.JokeUrl.ToString());
                if (string.IsNullOrEmpty(jokeString)) SaveSettings(SettingsEnum.JokeUrl.ToString(), JokeUrl);
                else JokeUrl = jokeString;
            }
            catch { }
        }

        private async void SaveSettings(string name, string value)
        {
            await _settingsService.SaveSettings(name, value);
        }
    }
}
