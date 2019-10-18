using A9MTE_Stys.Enums;
using A9MTE_Stys.Interfaces;
using A9MTE_Stys.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace A9MTE_Stys.ViewModels
{
    public class TronaldDumpQuotesPageViewModel : BindableBase
    {
        private readonly ITronaldDumpService _tronaldDumpService;
        private readonly IDatabaseService _databaseService;
        private readonly ISettingsService _settingsService;
        private readonly IToastMessage _toastMessage;

        private ObservableCollection<QuoteItem> quotes = new ObservableCollection<QuoteItem>();
        public ObservableCollection<QuoteItem> Quotes
        {
            get { return quotes; }
            set { SetProperty(ref quotes, value); }
        }

        private ScrollEnum scroll = ScrollEnum.Scroll;
        public ScrollEnum Scroll
        {
            get { return scroll; }
            set { SetProperty(ref scroll, value); }
        }

        public DelegateCommand OnRequestNewQuoteCommand { get; set; }
        public DelegateCommand<string> OnCellTappedCommand { get; set; }
        public DelegateCommand<QuoteItem> OnDeleteQuoteCommand { get; set; }

        public TronaldDumpQuotesPageViewModel(ITronaldDumpService tronaldDumpService, IDatabaseService databaseService, ISettingsService settingsService, IToastMessage toastMessage)
        {
            _tronaldDumpService = tronaldDumpService;
            _databaseService = databaseService;
            _settingsService = settingsService;
            _toastMessage = toastMessage;

            OnRequestNewQuoteCommand = new DelegateCommand(RequestQuote, CanRequestQuote);
            OnCellTappedCommand = new DelegateCommand<string>(ReadQuoteAsync, CanReadQuoteAsync);
            OnDeleteQuoteCommand = new DelegateCommand<QuoteItem>(DeleteQuote);

            var dbQuotes = _databaseService.GetQuotes();

            if (dbQuotes != null)
            {
                foreach (var item in dbQuotes) Quotes.Add(item);
            }

            if (!IsConnected()) _toastMessage.ShowToast("Not connected to internet!");
        }

        public async void RequestQuote()
        {
            var quotes = await _tronaldDumpService.GetJokeAsync();

            if (quotes != null)
            {
                var id = 1;

                if (Quotes.Count > 0) id = Quotes.Last().Id + 1;

                var quote = new QuoteItem
                {
                    Id = id,
                    Quote = quotes.value,
                    Tags = quotes.tags,
                    Icon = "trumpface.png"
                };

                Quotes.Add(quote);

                if (Device.RuntimePlatform == Device.Android) await _databaseService.AddQuote(quote);
            }
            else _toastMessage.ShowToast("Not possible to get quote!");
        }

        public bool CanRequestQuote()
        {
            return IsConnected();
        }

        public async void ReadQuoteAsync(string quote)
        {
            if (quote != null) await TextToSpeech.SpeakAsync(quote);
            else _toastMessage.ShowToast("No quote was selected!");
        }

        public bool CanReadQuoteAsync(string quote)
        {
            if (Quotes.Count != 0) return true;
            else return false;
        }

        public async void DeleteQuote(QuoteItem quote)
        {
            Scroll = ScrollEnum.NoScroll;

            if (quote != null) Quotes.Remove(quote);
            else _toastMessage.ShowToast("No quote was selected!");

            if (Device.RuntimePlatform == Device.Android) await _databaseService.DeleteQuote(quote);
        }

        private bool IsConnected() => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
