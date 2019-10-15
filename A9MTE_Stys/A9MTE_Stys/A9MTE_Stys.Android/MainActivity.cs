using A9MTE_Stys.Droid.Services;
using A9MTE_Stys.Interfaces;
using Android.App;
using Android.Content.PM;
using Android.OS;
using ImageCircle.Forms.Plugin.Droid;
using PanCardView.Droid;
using Prism;
using Prism.Ioc;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView.Droid;

namespace A9MTE_Stys.Droid
{
    [Activity(Label = "A9MTE_Stys", Icon = "@mipmap/ic_launcher", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.Essentials.Platform.Init(this, bundle);
            FormsMaterial.Init(this, bundle);
            ImageCircleRenderer.Init();
            CardsViewRenderer.Preserve();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(enableFastRenderer: true);
            LoadApplication(new App(new AndroidInitializer()));

            PancakeViewRenderer.Init();
        }
    }

    public class AndroidInitializer : IPlatformInitializer
    {
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<IToastMessage, ToastMessage>();
        }
    }
}

