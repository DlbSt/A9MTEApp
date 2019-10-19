using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace A9MTE_Stys.ViewModels
{
    public class LicensingPageViewModel : BindableBase
    {
        #region Services
        private readonly INavigationService _navigationService;
        #endregion

        #region Commands
        public DelegateCommand<string> ShowLicensePopUpCommand { get; set; }
        #endregion

        public LicensingPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ShowLicensePopUpCommand = new DelegateCommand<string>(ShowLicensePopUp);
        }

        #region Navigation
        private async void ShowLicensePopUp(string param)
        {
            await _navigationService.NavigateAsync("LicensePage", new NavigationParameters
            {
                { "libraryname", param },
            });
        }
        #endregion
    }
}
