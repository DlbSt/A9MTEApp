using A9MTE_Stys.Enums;
using A9MTE_Stys.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace A9MTE_Stys.ViewModels
{
    public class TronaldDumpMemePageViewModel : BindableBase
    {
        #region Services
        private readonly ITronaldDumpService _tronaldDumpService;
        private readonly IDatabaseService _databaseService;
        private readonly ISettingsService _settingsService;
        private readonly IToastMessage _toastMessage;
        #endregion

        #region Commands
        public DelegateCommand OnAddMemeCommand { get; set; }
        #endregion

        #region Properties
        private ObservableCollection<ImageSource> imageCollection = new ObservableCollection<ImageSource>();
        public ObservableCollection<ImageSource> ImageCollection
        {
            get { return imageCollection; }
            set { SetProperty(ref imageCollection, value); }
        }
        #endregion

        #region Fields
        private const int trumpImageCount = 5;
        private string url;
        private const string apiErrorMessage = "API Url not valid!";
        private const string commonErrorMessage = "Not possible to get meme!";
        #endregion

        public TronaldDumpMemePageViewModel(ITronaldDumpService tronaldDumpService, IDatabaseService databaseService, ISettingsService settingsService, IToastMessage toastMessage)
        {
            _tronaldDumpService = tronaldDumpService;
            _databaseService = databaseService;
            _settingsService = settingsService;
            _toastMessage = toastMessage;

            OnAddMemeCommand = new DelegateCommand(AddMemeAsync, CanAddMemeAsync);

            GetMemeUrl();
        }

        #region ListViewHandling
        public async void AddMemeAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    var value = await _tronaldDumpService.GetMemeAsync(url);
                    if (value != null)
                    {
                        if (ImageCollection.Count == trumpImageCount) ImageCollection.RemoveAt(0);
                        ImageCollection.Add(value);
                    }
                    else
                    {
                        _toastMessage.ShowToast(commonErrorMessage);
                    }
                }
                else _toastMessage.ShowToast(apiErrorMessage);
            }
            catch { _toastMessage.ShowToast(commonErrorMessage); }
        }

        public bool CanAddMemeAsync()
        {
            return IsConnected();
        }
        #endregion

        #region HelperMethods
        public async void GetMemeUrl()
        {
            url = await _settingsService.LoadSettings(SettingsEnum.MemeUrl.ToString());
        }

        private bool IsConnected() => Connectivity.NetworkAccess == NetworkAccess.Internet;
        #endregion
    }
}
