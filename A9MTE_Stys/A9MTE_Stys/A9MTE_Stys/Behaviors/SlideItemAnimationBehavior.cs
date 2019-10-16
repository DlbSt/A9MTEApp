using Prism.Behaviors;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace A9MTE_Stys.Behaviors
{
    public class SlideItemAnimationBehavior : BehaviorBase<PancakeView>
    {
        protected override void OnAttachedTo(PancakeView bindable)
        {
            base.OnAttachedTo(bindable);

            if (Device.RuntimePlatform == Device.UWP)
            {
                if (bindable.GestureRecognizers[1] is SwipeGestureRecognizer)
                {
                    ((SwipeGestureRecognizer)bindable.GestureRecognizers[1]).Swiped += SlideItemAnimationBehavior_Swiped;
                }
            }
        }

        protected override void OnDetachingFrom(PancakeView bindable)
        {
            base.OnDetachingFrom(bindable);

            if (Device.RuntimePlatform == Device.UWP)
            {
                if (bindable.GestureRecognizers[1] is SwipeGestureRecognizer)
                {
                    ((SwipeGestureRecognizer)bindable.GestureRecognizers[1]).Swiped -= SlideItemAnimationBehavior_Swiped;
                }
            }
        }

        private void SlideItemAnimationBehavior_Swiped(object sender, SwipedEventArgs e)
        {
            var trX = ((PancakeView)sender).TranslationX;
            var trY = ((PancakeView)sender).TranslationY;

            if (e.Direction == SwipeDirection.Left)
            {
                ((PancakeView)sender).TranslateTo(trX - 200, trY, 500);
            }
            else if (e.Direction == SwipeDirection.Right)
            {
                ((PancakeView)sender).TranslateTo(trX + 200, trY, 500);
            }
            else if (e.Direction != SwipeDirection.Up && e.Direction != SwipeDirection.Down)
            {
                ((PancakeView)sender).TranslateTo(trX + 200, trY, 500);

                Task.Delay(500);
            }
        }

    }
}
