using Prism;
using Prism.Ioc;
using A9MTE_Stys.ViewModels;
using A9MTE_Stys.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using A9MTE_Stys.Services;
using A9MTE_Stys.Interfaces;
using Prism.Plugin.Popups;
using A9MTE_Stys.Views.PopUps;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace A9MTE_Stys
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("MasterDetailedPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterPopupNavigationService();

            containerRegistry.Register<IJokeService, JokeService>();
            containerRegistry.Register<ITronaldDumpService, TronaldDumpService>();
            containerRegistry.Register<IDatabaseService, DatabaseService>();
            containerRegistry.Register<ISettingsService, SettingsService>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MasterDetailedPage, MasterDetailedPageViewModel>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<ChuckJokesPage, ChuckJokesPageViewModel>();
            containerRegistry.RegisterForNavigation<TronaldDumpPage, TronaldDumpPageViewModel>();
            containerRegistry.RegisterForNavigation<TronaldDumpMemePage, TronaldDumpMemePageViewModel>();
            containerRegistry.RegisterForNavigation<SettingsPage, SettingsPageViewModel>();
            containerRegistry.RegisterForNavigation<TronaldDumpQuotesPage, TronaldDumpQuotesPageViewModel>();
            containerRegistry.RegisterForNavigation<TextPickerPage, TextPickerPageViewModel>();
        }
    }
}
