using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace A9MTE_Stys.ViewModels.PopUps
{
    public class AboutPageViewModel : BindableBase
    {
        #region Services
        private readonly INavigationService _navigationService;
        #endregion

        #region Commands
        public DelegateCommand ClosePopUpCommand { get; set; }
        #endregion

        public AboutPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ClosePopUpCommand = new DelegateCommand(ClosePopUpAsync);
        }

        #region Navigation
        private async void ClosePopUpAsync()
        {
            await _navigationService.GoBackAsync();
        }
        #endregion
    }
}
