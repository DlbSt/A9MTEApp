using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using A9MTE_Stys.Droid.Services;
using A9MTE_Stys.Interfaces;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

[assembly: Xamarin.Forms.Dependency(typeof(ToastMessage))]
namespace A9MTE_Stys.Droid.Services
{
    public class ToastMessage : IToastMessage
    {
        public void ShowToast(string toastMessage)
        {
            Toast.MakeText(Application.Context, toastMessage, ToastLength.Long).Show();
        }
    }
}