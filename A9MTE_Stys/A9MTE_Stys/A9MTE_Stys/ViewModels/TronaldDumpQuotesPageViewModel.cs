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

            if (!IsConnected()) _toastMessage.ShowToast("Not connected to internet!");
        }

        public async void RequestQuote()
        {
            var quote = await _tronaldDumpService.GetJokeAsync();

            if (quote != null)
            {
                var id = 1;

                if (Quotes.Count > 0) id = Quotes.Count + 1;

                Quotes.Add(new QuoteItem {
                    Id = id,
                    Quote = quote.value,
                    Tags = quote.tags,
                    Icon = "trumpface.png" });
            }
            else
            {
                _toastMessage.ShowToast("Not possible to get quote!");
            }
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

        public void DeleteQuote(QuoteItem quote)
        {
            Scroll = ScrollEnum.NoScroll;

            if (quote != null) Quotes.Remove(quote);
            else _toastMessage.ShowToast("No quote was selected!");
        }

        private bool IsConnected() => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}
