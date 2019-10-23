using A9MTE_Stys.Enums;
using A9MTE_Stys.Extensions;
using A9MTE_Stys.Interfaces;
using A9MTE_Stys.Model;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.IO;
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
        public DelegateCommand OnDeleteMemeCommand { get; set; }
        #endregion

        #region Properties
        private ObservableCollection<MemeDbItem> imageCollection = new ObservableCollection<MemeDbItem>();
        public ObservableCollection<MemeDbItem> ImageCollection
        {
            get { return imageCollection; }
            set { SetProperty(ref imageCollection, value); }
            
        }

        private int imageIndex;
        public int ImageIndex
        {
            get { return imageIndex; }
            set { SetProperty(ref imageIndex, value); }
        }
        #endregion

        #region Fields
        private int countLimit = 5;
        private string url;
        private const string apiErrorMessage = "API Url not valid!";
        private const string commonErrorMessage = "Not possible to get meme!";
        private const string picNotFoundMessage = "Picture not found!";
        #endregion

        public TronaldDumpMemePageViewModel(ITronaldDumpService tronaldDumpService, IDatabaseService databaseService, ISettingsService settingsService, IToastMessage toastMessage)
        {
            _tronaldDumpService = tronaldDumpService;
            _databaseService = databaseService;
            _settingsService = settingsService;
            _toastMessage = toastMessage;

            OnAddMemeCommand = new DelegateCommand(AddMemeAsync, CanAddMemeAsync);
            OnDeleteMemeCommand = new DelegateCommand(DeleteMeme, CanDeleteMeme);

            ImageCollection.CollectionChanged += ImageCollection_CollectionChanged;

            var dbJokes = _databaseService.GetMemes();

            if (dbJokes != null)
            {
                foreach (var item in dbJokes) ImageCollection.Add(item);
            }

            LoadSettings();
        }

        #region CarouselHandling
        public async void AddMemeAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(url))
                {
                    var value = await _tronaldDumpService.GetMemeAsync(url);
                    if (value != null)
                    {
                        var id = 1;

                        if (ImageCollection.Count > 0) id = ImageCollection.Last().Id + 1;

                        var meme = new MemeDbItem
                        {
                            Id = id,
                            Image = value
                        };

                        ImageCollection.Add(meme);
                        if (Device.RuntimePlatform == Device.Android || Device.RuntimePlatform == Device.UWP) await _databaseService.AddMeme(meme);
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
            return ExtensionMethods.IsConnected() && ImageCollection.Count < countLimit;
        }

        public async void DeleteMeme()
        {
            try
            {
                await _databaseService.DeleteMeme(ImageCollection[ImageIndex]);
                ImageCollection.RemoveAt(ImageIndex);
            }
            catch { _toastMessage.ShowToast(picNotFoundMessage); }
        }

        public bool CanDeleteMeme() => ImageCollection.Count != 0 ? true : false;

        private void ImageCollection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnAddMemeCommand.RaiseCanExecuteChanged();
            OnDeleteMemeCommand.RaiseCanExecuteChanged();
        }
        #endregion

        #region Settings
        private async void LoadSettings()
        {
            try
            {
                url = await _settingsService.LoadSettings(SettingsEnum.MemeUrl.ToString());
                var limitString = await _settingsService.LoadSettings(SettingsEnum.MemeLimit.ToString());
                countLimit = Convert.ToInt32(limitString);
            }
            catch { }
        }
        #endregion

        #region HelperMethods
        public static ImageSource ByteArrayToImage(byte[] imageBytes)
        {
            return ImageSource.FromStream(() => new MemoryStream(imageBytes));
        }
        #endregion
    }
}
