using A9MTE_Stys.Enums;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace A9MTE_Stys.ViewModels
{
    public class TextPickerPageViewModel : BindableBase, IInitialize
    {
        private readonly INavigationService _navigationService;

        private string url = string.Empty;
        public string Url
        {
            get { return url; }
            set { SetProperty(ref url, value); }
        }

        public DelegateCommand OnOKClickedCommand { get; set; }
        public DelegateCommand OnCancelClickedCommand { get; set; }

        private PopUpTypeEnum popUpType;

        public TextPickerPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            OnOKClickedCommand = new DelegateCommand(OKClickedAsync);
            OnCancelClickedCommand = new DelegateCommand(CancelClickedAsync);
        }

        public async void OKClickedAsync()
        {
            await _navigationService.GoBackAsync(new NavigationParameters{
                { "button", PopUpButtonEnum.OK.ToString() },
                { "typeenum", popUpType.ToString() },
                { "url", Url }
            });
        }

        public async void CancelClickedAsync()
        {
            await _navigationService.GoBackAsync(new NavigationParameters{
                { "button", PopUpButtonEnum.Cancel.ToString() },
                { "typeenum", popUpType.ToString() },
                { "url", Url }
            });
        }

        public void Initialize(INavigationParameters parameters)
        {
            Url = parameters["url"] as string;
            popUpType = (PopUpTypeEnum)Enum.Parse(typeof(PopUpTypeEnum), parameters["type"] as string);
        }
    }
}
