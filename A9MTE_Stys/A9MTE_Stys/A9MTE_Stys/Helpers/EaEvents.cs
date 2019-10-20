using Prism.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace A9MTE_Stys.Helpers
{
    public class PopUpResultEvent : PubSubEvent<PopUpResultData> { }
    public class TrumpLimitEvent : PubSubEvent<LimitPopupResultData> { }
}
