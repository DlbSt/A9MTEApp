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
        #region Services
        private readonly ITronaldDumpService _tronaldDumpService;
        private readonly IDatabaseService _databaseService;
        private readonly ISettingsService _settingsService;
        private readonly IToastMessage _toastMessage;
        #endregion

        #region Commands
        public DelegateCommand OnRequestNewQuoteCommand { get; set; }
        public DelegateCommand<string> OnCellTappedCommand { get; set; }
        public DelegateCommand<QuoteItem> OnDeleteQuoteCommand { get; set; }
        #endregion

        #region Properties
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
        #endregion

        #region Fields
        private string quoteUrl;
        private const string apiErrorMessage = "API Url not valid!";
        private const string commonErrorMessage = "Not possible to get quote!";
        private const string selectErrorMessage = "No quote was selected!";
        private const string notConnectedMessage = "Not connected to internet!";
        private const string iconUrl = "trumpface.png";
        #endregion

        public TronaldDumpQuotesPageViewModel(ITronaldDumpService tronaldDumpService, IDatabaseService databaseService, ISettingsService settingsService, IToastMessage toastMessage)
        {
            _tronaldDumpService = tronaldDumpService;
            _databaseService = databaseService;
            _settingsService = settingsService;
            _toastMessage = toastMessage;

            OnRequestNewQuoteCommand = new DelegateCommand(RequestQuote, CanRequestQuote);
            OnCellTappedCommand = new DelegateCommand<string>(ReadQuoteAsync, CanReadQuoteAsync);
            OnDeleteQuoteCommand = new DelegateCommand<QuoteItem>(DeleteQuote);

            GetQuoteUrl();

            var dbQuotes = _databaseService.GetQuotes();

            if (dbQuotes != null)
            {
                foreach (var item in dbQuotes) Quotes.Add(new QuoteItem { Tags = item.Tags.Split(';').ToList(),
                                                                          Icon = item.Icon,
                                                                          Id = item.Id,
                                                                          Quote = item.Quote
                });
            }

            if (!IsConnected()) _toastMessage.ShowToast(notConnectedMessage);
        }

        #region ListViewHandling
        public async void RequestQuote()
        {
            try
            {
                if (!string.IsNullOrEmpty(quoteUrl))
                {
                    var quotes = await _tronaldDumpService.GetQuoteAsync(quoteUrl);

                    if (quotes != null)
                    {
                        var id = 1;

                        if (Quotes.Count > 0) id = Quotes.Last().Id + 1;

                        var quote = new QuoteItem
                        {
                            Id = id,
                            Quote = quotes.value,
                            Tags = quotes.tags,
                            Icon = iconUrl
                        };

                        Quotes.Add(quote);

                        if (Device.RuntimePlatform == Device.Android) await _databaseService.AddQuote(new QuoteDbItem { Icon = quote.Icon,
                                                                                                                        Id = quote.Id, 
                                                                                                                        Quote = quote.Quote,
                                                                                                                        Tags = String.Join(";", quote.Tags.ToArray())
                        });
                    }
                    else _toastMessage.ShowToast(commonErrorMessage);
                }
                else _toastMessage.ShowToast(apiErrorMessage);
            }
            catch { _toastMessage.ShowToast(commonErrorMessage); }
        }

        public bool CanRequestQuote()
        {
            return IsConnected();
        }

        public async void ReadQuoteAsync(string quote)
        {
            if (quote != null) await TextToSpeech.SpeakAsync(quote);
            else _toastMessage.ShowToast(selectErrorMessage);
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
            else _toastMessage.ShowToast(selectErrorMessage);

            if (Device.RuntimePlatform == Device.Android) await _databaseService.DeleteQuote(new QuoteDbItem { Icon = quote.Icon,
                                                                                                               Id = quote.Id,
                                                                                                               Quote = quote.Quote,
                                                                                                               Tags = String.Join(";", quote.Tags.ToArray())
            });
        }
        #endregion

        #region HelperMethods
        public async void GetQuoteUrl()
        {
            quoteUrl = await _settingsService.LoadSettings(SettingsEnum.QuoteUrl.ToString());
        }

        private bool IsConnected() => Connectivity.NetworkAccess == NetworkAccess.Internet;
        #endregion
    }
}
