using A9MTE_Stys.Enums;
using A9MTE_Stys.Model;
using A9MTE_Stys.ViewModels;
using A9MTE_Stys.Views;
using Prism.Behaviors;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace A9MTE_Stys.Behaviors
{
    public class ListViewScrollBehavior : BehaviorBase<ListView>
    {
        ListView listView;

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            if (Device.RuntimePlatform == Device.Android)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    listView = bindable;

                    var itemSource = (ObservableCollection<JokeItem>)AssociatedObject.ItemsSource;
                    itemSource.CollectionChanged += ListViewScrollBehavior_CollectionChanged;
                });
            }
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            base.OnDetachingFrom(bindable);


            if (Device.RuntimePlatform == Device.Android)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var itemSource = (ObservableCollection<JokeItem>)AssociatedObject.ItemsSource;
                    itemSource.CollectionChanged -= ListViewScrollBehavior_CollectionChanged;
                });
            }
        }

        private void ListViewScrollBehavior_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {

            if (Device.RuntimePlatform == Device.Android)
            {
                ChuckJokesPage view = (ChuckJokesPage)AssociatedObject.Parent.Parent;
                ChuckJokesPageViewModel viewModel = null;
                if (view != null)
                {
                    viewModel = view.BindingContext as ChuckJokesPageViewModel;
                }

                if (viewModel != null)
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        try
                        {
                            if (viewModel.Scroll == ScrollEnum.Scroll)
                            {
                                var item = (sender as ObservableCollection<JokeItem>).Last();
                                listView.ScrollTo(item, ScrollToPosition.MakeVisible, false);

                            }
                        }
                        catch { }
                    });
                }
            }
        }
    }
}
