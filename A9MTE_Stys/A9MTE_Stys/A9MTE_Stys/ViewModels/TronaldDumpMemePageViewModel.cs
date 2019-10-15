using A9MTE_Stys.Interfaces;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;

namespace A9MTE_Stys.ViewModels
{
    public class TronaldDumpMemePageViewModel : BindableBase
    {
        private readonly ITronaldDumpService _tronaldDumpService;
        private readonly IDatabaseService _databaseService;
        private readonly ISettingsService _settingsService;
        private readonly IToastMessage _toastMessage;

        private const int trumpImageCount = 5;

        public DelegateCommand OnAddMemeCommand { get; set; }

        private ObservableCollection<ImageSource> imageCollection = new ObservableCollection<ImageSource>();
        public ObservableCollection<ImageSource> ImageCollection
        {
            get { return imageCollection; }
            set { SetProperty(ref imageCollection, value); }
        }

        public TronaldDumpMemePageViewModel(ITronaldDumpService tronaldDumpService, IDatabaseService databaseService, ISettingsService settingsService, IToastMessage toastMessage)
        {
            _tronaldDumpService = tronaldDumpService;
            _databaseService = databaseService;
            _settingsService = settingsService;
            _toastMessage = toastMessage;

            OnAddMemeCommand = new DelegateCommand(AddMemeAsync, CanAddMemeAsync);
        }

        public bool CanAddMemeAsync()
        {
            return true;
        }

        public async void AddMemeAsync()
        {
            var value = await _tronaldDumpService.GetMemeAsync();
            if (value != null)
            {
                if (ImageCollection.Count == trumpImageCount) ImageCollection.RemoveAt(0);
                ImageCollection.Add(value);
            }
            else
            {
                _toastMessage.ShowToast("Not possible to get meme!");
            }

        }
    }
}
