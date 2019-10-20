using A9MTE_Stys.Enums;
using A9MTE_Stys.Helpers;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace A9MTE_Stys.ViewModels.PopUps
{
    public class TrumpLimitPageViewModel : BindableBase
    {
        #region Services
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #region Commands
        public DelegateCommand OnOKClickedCommand { get; set; }
        public DelegateCommand OnCancelClickedCommand { get; set; }
        #endregion

        #region Properties
        private int minimum = 5;
        public int Minimum
        {
            get { return minimum; }
            set { SetProperty(ref minimum, value); }
        }

        private int maximum = 10;
        public int Maximum
        {
            get { return maximum; }
            set { SetProperty(ref maximum, value); }
        }

        private int limitValue = 5;
        public int LimitValue
        {
            get { return limitValue; }
            set { SetProperty(ref limitValue, value); }
        }
        #endregion

        public TrumpLimitPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            OnOKClickedCommand = new DelegateCommand(OKClickedAsync);
            OnCancelClickedCommand = new DelegateCommand(CancelClickedAsync);
        }

        private async void OKClickedAsync()
        {
            _eventAggregator.GetEvent<TrumpLimitEvent>().Publish(new LimitPopupResultData 
            { 
                LimitValue = LimitValue,
                Result = PopUpResultEnum.OK 
            });

            await _navigationService.GoBackAsync();
        }

        private async void CancelClickedAsync()
        {
            _eventAggregator.GetEvent<TrumpLimitEvent>().Publish(new LimitPopupResultData
            {
                LimitValue = LimitValue,
                Result = PopUpResultEnum.Cancel
            });

            await _navigationService.GoBackAsync();
        }
    }
}
