using A9MTE_Stys.Enums;
using A9MTE_Stys.Helpers;
using Prism.Commands;
using Prism.Events;
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
        #region Services
        private readonly INavigationService _navigationService;
        private readonly IEventAggregator _eventAggregator;
        #endregion

        #region Commands
        public DelegateCommand OnOKClickedCommand { get; set; }
        public DelegateCommand OnCancelClickedCommand { get; set; }
        #endregion

        #region Properties
        private string url = string.Empty;
        public string Url
        {
            get { return url; }
            set { SetProperty(ref url, value); }
        }
        #endregion

        #region Fields
        private PopUpTypeEnum popUpType;
        #endregion

        public TextPickerPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;

            OnOKClickedCommand = new DelegateCommand(OKClickedAsync);
            OnCancelClickedCommand = new DelegateCommand(CancelClickedAsync);
        }

        #region Navigation
        public void Initialize(INavigationParameters parameters)
        {
            Url = parameters["url"] as string;
            popUpType = (PopUpTypeEnum)Enum.Parse(typeof(PopUpTypeEnum), parameters["type"] as string);
        }

        public async void OKClickedAsync()
        {
            _eventAggregator.GetEvent<PopUpResultEvent>().Publish(new PopUpResultData 
            { 
                Result = PopUpResultEnum.OK,
                Type = popUpType,
                Url = Url
            });

            await _navigationService.GoBackAsync();
        }

        public async void CancelClickedAsync()
        {
            _eventAggregator.GetEvent<PopUpResultEvent>().Publish(new PopUpResultData
            {
                Result = PopUpResultEnum.Cancel,
                Type = popUpType,
                Url = Url
            });

            await _navigationService.GoBackAsync();
        }
        #endregion
    }
}
