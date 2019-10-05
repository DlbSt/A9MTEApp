using A9MTE_Stys.Model;
using A9MTE_Stys.Views;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace A9MTE_Stys.ViewModels
{
    public class MasterDetailedPageViewModel : BindableBase
    {
        INavigationService _navigationService;
        public DelegateCommand<MenuItem> OnNavigateCommand { get; set; }
        public List<MenuItem> MenuList { get; set; } = new List<MenuItem> { new MenuItem { Name = "Home", IconSource = "home.svg", TargetType = typeof(HomePage) },
                                                                            new MenuItem { Name = "Chuck Jokes API", IconSource = "restapi.svg", TargetType = typeof(ChuckJokesPage) },
                                                                            new MenuItem { Name = "Tronald Dump API", IconSource = "trump.svg", TargetType = typeof(TronaldDumpPage) },
                                                                            new MenuItem { Name = "Settings", IconSource = "settings.svg", TargetType = typeof(SettingsPage) } };

        private MenuItem selectedPage;
        public MenuItem SelectedPage
        {
            get { return selectedPage; }
            set { SetProperty(ref selectedPage, value); }
        }

        public MasterDetailedPageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            OnNavigateCommand = new DelegateCommand<MenuItem>(NavigateAsync);
            SelectedPage = MenuList[0];
        }

        private async void NavigateAsync(MenuItem item)
        {
            await _navigationService.NavigateAsync("NavigationPage/" + item.TargetType.Name);
        }
    }
}
