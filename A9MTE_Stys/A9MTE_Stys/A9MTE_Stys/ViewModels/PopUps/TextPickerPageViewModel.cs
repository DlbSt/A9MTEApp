using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace A9MTE_Stys.ViewModels
{
    public class TextPickerPageViewModel : BindableBase, INavigationAware
    {
        public TextPickerPageViewModel()
        {

        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            Debug.WriteLine("");
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            Debug.WriteLine("");
        }
    }
}
