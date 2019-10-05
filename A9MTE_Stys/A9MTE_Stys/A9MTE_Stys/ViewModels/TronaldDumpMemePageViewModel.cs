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

        private const int trumpImageCount = 5;

        public DelegateCommand OnAddMemeCommand { get; set; }

        private ObservableCollection<ImageSource> imageCollection = new ObservableCollection<ImageSource>();
        public ObservableCollection<ImageSource> ImageCollection
        {
            get { return imageCollection; }
            set { SetProperty(ref imageCollection, value); }
        }

        public TronaldDumpMemePageViewModel(ITronaldDumpService tronaldDumpService)
        {
            _tronaldDumpService = tronaldDumpService;

            OnAddMemeCommand = new DelegateCommand(AddMemeAsync, CanAddMemeAsync);

            //GetRandomQuote();
        }

        public bool CanAddMemeAsync()
        {
            return true;
        }

        public async void AddMemeAsync()
        {
            var value = await _tronaldDumpService.GetMemeAsync();
            if (ImageCollection.Count == trumpImageCount) ImageCollection.RemoveAt(0);
            ImageCollection.Add(value);
        }

        private async void GetRandomQuote()
        {
            var value = await _tronaldDumpService.GetJokeAsync();
            Debug.WriteLine(value);
        }
    }
}
