using A9MTE_Stys.Interfaces;
using A9MTE_Stys.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace A9MTE_Stys.ViewModels
{
    public class TronaldDumpQuotesPageViewModel : BindableBase
    {
        private readonly ITronaldDumpService _tronaldDumpService;

        private ObservableCollection<QuoteItem> quotes = new ObservableCollection<QuoteItem>();
        public ObservableCollection<QuoteItem> Quotes
        {
            get { return quotes; }
            set { SetProperty(ref quotes, value); }
        }

        public DelegateCommand OnRequestNewQuoteCommand { get; set; }

        public TronaldDumpQuotesPageViewModel(ITronaldDumpService tronaldDumpService)
        {
            _tronaldDumpService = tronaldDumpService;

            OnRequestNewQuoteCommand = new DelegateCommand(RequestQuote, CanRequestQuote);
        }

        public async void RequestQuote()
        {
            var quote = await _tronaldDumpService.GetJokeAsync();
            Quotes.Add(new QuoteItem { Quote = quote.value, Tags = quote.tags  });
        }

        public bool CanRequestQuote()
        {
            return true;
        }
    }
}
