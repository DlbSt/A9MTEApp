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
    public class ChuckJokesPageViewModel : BindableBase
    {
        private readonly IJokeService _jokeService;
        private readonly IDatabaseService _databaseService;
        private readonly ISettingsService _settingsService;
        private readonly IToastMessage _toastMessage;

        private Random random = new Random();
        private string url = "https://api.chucknorris.io/jokes/random?category=";

        public DelegateCommand OnAddJokeCommand { get; set; }
        public DelegateCommand<string> OnCellTappedCommand { get; set; }
        public DelegateCommand<JokeItem> OnDeleteJokeCommand { get; set; }

        private List<JokeItem> jokeList = new List<JokeItem>();
        public List<JokeItem> JokeList
        {
            get { return jokeList; }
            set { SetProperty(ref jokeList, value); }
        }

        private ObservableCollection<JokeItem> filteredCollection = new ObservableCollection<JokeItem>();
        public ObservableCollection<JokeItem> FilteredCollection
        {
            get { return filteredCollection; }
            set { SetProperty(ref filteredCollection, value); }
        }

        private List<string> categories = new List<string>(Enum.GetNames(typeof(CategoryEnum)));
        public List<string> Categories
        {
            get { return categories; }
            set { SetProperty(ref categories, value); }
        }

        private CategoryEnum showCategory = CategoryEnum.All;
        public CategoryEnum ShowCategory
        {
            get { return showCategory; }
            set
            {
                SetProperty(ref showCategory, value);
                Scroll = ScrollEnum.NoScroll;
                UpdateBindedCollection();
            }
        }

        private CategoryEnum category = CategoryEnum.Animal;
        public CategoryEnum Category
        {
            get { return category; }
            set { SetProperty(ref category, value); }
        }

        private ScrollEnum scroll = ScrollEnum.Scroll;
        public ScrollEnum Scroll
        {
            get { return scroll; }
            set { SetProperty(ref scroll, value); }
        }

        public ChuckJokesPageViewModel(IJokeService jokeService, IDatabaseService databaseService, ISettingsService settingsService, IToastMessage toastMessage)
        {
            _jokeService = jokeService;
            _databaseService = databaseService;
            _settingsService = settingsService;
            _toastMessage = toastMessage;

            OnAddJokeCommand = new DelegateCommand(AddJokeAsync, CanAddJokeAsync);
            OnCellTappedCommand = new DelegateCommand<string>(ReadJokeAsync, CanReadJokeAsync);
            OnDeleteJokeCommand = new DelegateCommand<JokeItem>(DeleteJoke);

            LoadUrl();

            var dbJokes = _databaseService.GetJokes();

            if (dbJokes != null)
            {
                foreach (var item in dbJokes) JokeList.Add(item);
            }

            UpdateBindedCollection();

            if (!IsConnected()) _toastMessage.ShowToast("Not connected to internet!");
        }

        private async void LoadUrl()
        {
            var urlString = await _settingsService.LoadSettings(SettingsEnum.JokeUrl.ToString());
            if (string.IsNullOrEmpty(urlString)) await _settingsService.SaveSettings(SettingsEnum.JokeUrl.ToString(), url);
            else url = urlString;
        }

        private void UpdateBindedCollection()
        {
            if (ShowCategory == CategoryEnum.All)
            {
                //FilteredCollection.Clear();
                FilteredCollection = new ObservableCollection<JokeItem>();

                foreach (var item in JokeList)
                {
                    FilteredCollection.Add(item);
                }
            }
            else
            {
                //FilteredCollection.Clear();
                FilteredCollection = new ObservableCollection<JokeItem>();

                foreach (var item in JokeList.Where(joke => joke.Category == ShowCategory))
                {
                    FilteredCollection.Add(item);
                }
            }
        }

        public async void ReadJokeAsync(string joke)
        {
            if (joke != null)
            {
                await TextToSpeech.SpeakAsync(joke);
            }
            else _toastMessage.ShowToast("No joke was selected!");
        }

        private bool CanReadJokeAsync(string joke)
        {
            if (FilteredCollection.Count != 0) return true;
            else return false;
        }

        public async void AddJokeAsync()
        {
            Scroll = ScrollEnum.Scroll;
            var actualCategory = (Category == 0) ? (CategoryEnum)random.Next(1, 16) : Category;

            var joke = await _jokeService.GetJokeAsync(url, actualCategory.ToString().ToLower()); //if category is "All", select random one
            if (joke != null)
            {
                int id = 1;

                if (JokeList.Count > 0) id = JokeList.Last().Id + 1;

                var jokeItem = new JokeItem
                {
                    Id = id,
                    Icon = "chucknorris.png",
                    Joke = joke.value,
                    Url = new Uri(joke.url),
                    RestId = joke.icon_url,
                    Category = actualCategory
                };
                JokeList.Add(jokeItem);
                UpdateBindedCollection();
                if (Device.RuntimePlatform == Device.Android) _databaseService.AddJoke(jokeItem);
            }
            else _toastMessage.ShowToast("Joke couldn't be added!");
        }

        public void DeleteJoke(JokeItem joke)
        {
            Scroll = ScrollEnum.NoScroll;

            JokeList.Remove(joke);
            UpdateBindedCollection();
            if (Device.RuntimePlatform == Device.Android) _databaseService.DeleteJoke(joke);
        }
        public bool CanAddJokeAsync() => IsConnected();
        private bool IsConnected() => Connectivity.NetworkAccess == NetworkAccess.Internet;
    }
}

