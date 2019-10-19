﻿using A9MTE_Stys.Enums;
using A9MTE_Stys.Helpers;
using A9MTE_Stys.Interfaces;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace A9MTE_Stys.ViewModels
{
    public class SettingsPageViewModel : BindableBase
    {
        #region DependencyInjection
        private readonly IPageDialogService _dialogService;
        private readonly ISettingsService _settingsService;
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #region Commands
        public DelegateCommand<string> ShowJokeAddressPopUpCommand { get; set; }
        public DelegateCommand<string> ShowMemeAddressPopUpCommand { get; set; }
        public DelegateCommand<string> ShowQuoteAddressPopUpCommand { get; set; }
        #endregion

        #region Properties
        private string jokeUrl;
        public string JokeUrl
        {
            get { return jokeUrl; }
            set
            {
                SetProperty(ref jokeUrl, value);
                SaveSettings(SettingsEnum.JokeUrl.ToString(), value);
            }
        }

        private string memeUrl;
        public string MemeUrl
        {
            get { return memeUrl; }
            set
            {
                SetProperty(ref memeUrl, value);
                SaveSettings(SettingsEnum.MemeUrl.ToString(), value);
            }
        }

        private string quoteUrl;
        public string QuoteUrl
        {
            get { return quoteUrl; }
            set
            {
                SetProperty(ref quoteUrl, value);
                SaveSettings(SettingsEnum.QuoteUrl.ToString(), value);
            }
        }
        #endregion

        public SettingsPageViewModel(INavigationService navigationService,
                                     IEventAggregator eventAggregator,
                                     IPageDialogService dialogService,
                                     ISettingsService settingsService)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _dialogService = dialogService;
            _settingsService = settingsService;

            ShowJokeAddressPopUpCommand = new DelegateCommand<string>(ShowJokeAddressPopUpWindowAsync);
            ShowMemeAddressPopUpCommand = new DelegateCommand<string>(ShowMemeAddressPopUpWindowAsync);
            ShowQuoteAddressPopUpCommand = new DelegateCommand<string>(ShowQuoteAddressPopUpWindowAsync);

            _eventAggregator.GetEvent<PopUpResultEvent>().Subscribe(UpdateUrl);

            LoadSettings();
        }

        #region ShowPopUp
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
        #endregion

        #region EventAggregator
        private void UpdateUrl(PopUpResultData resultData)
        {
            if (resultData.Result == PopUpResultEnum.OK)
            {
                if (resultData.Type == PopUpTypeEnum.ChuckJokes)
                {
                    if (!string.IsNullOrEmpty(resultData.Url)) JokeUrl = resultData.Url;
                }
                else if (resultData.Type == PopUpTypeEnum.TrumpMemes)
                {
                    if (!string.IsNullOrEmpty(resultData.Url)) MemeUrl = resultData.Url;
                }
                else
                {
                    if (!string.IsNullOrEmpty(resultData.Url)) QuoteUrl = resultData.Url;
                }
            }
        }
        #endregion

        #region Settings
        private async void LoadSettings()
        {
            try
            {
                JokeUrl = await _settingsService.LoadSettings(SettingsEnum.JokeUrl.ToString());
                MemeUrl = await _settingsService.LoadSettings(SettingsEnum.MemeUrl.ToString());
                QuoteUrl = await _settingsService.LoadSettings(SettingsEnum.QuoteUrl.ToString());
            }
            catch { }
        }

        private async void SaveSettings(string name, string value)
        {
            await _settingsService.SaveSettings(name, value);
        }
        #endregion
    }
}
