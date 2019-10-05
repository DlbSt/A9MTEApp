using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace A9MTE_Stys.Model
{
    public class TrumpMeme
    {
        [JsonConverter(typeof(BinaryConverter))]
        public Image accept { get; set; }
    }
}
