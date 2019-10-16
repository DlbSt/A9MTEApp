using Prism.Behaviors;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.PancakeView;

namespace A9MTE_Stys.Behaviors
{
    public class TappedItemAnimationBehavior : BehaviorBase<PancakeView>
    {
        protected override void OnAttachedTo(PancakeView bindable)
        {
            base.OnAttachedTo(bindable);

            if (bindable.GestureRecognizers[0] is TapGestureRecognizer)
            {
                ((TapGestureRecognizer)bindable.GestureRecognizers[0]).Tapped += TappedItemAnimationBehavior_Tapped;
            }
            
        }

        protected override void OnDetachingFrom(PancakeView bindable)
        {
            base.OnDetachingFrom(bindable);

            if (bindable.GestureRecognizers[0] is TapGestureRecognizer)
            {
                ((TapGestureRecognizer)bindable.GestureRecognizers[0]).Tapped -= TappedItemAnimationBehavior_Tapped;
            }
        }

        private async void TappedItemAnimationBehavior_Tapped(object sender, EventArgs e)
        {
            var scale = ((PancakeView)sender).Scale;

            await ((PancakeView)sender).ScaleTo(scale * 0.9, 100);//FadeTo(0.3, 10);
            await ((PancakeView)sender).ScaleTo(scale, 100);//FadeTo(1, 500);
        }
    }
}
