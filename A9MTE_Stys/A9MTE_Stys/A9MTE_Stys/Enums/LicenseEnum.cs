using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace A9MTE_Stys.Enums
{
    public enum LicenseEnum
    {
        [Display(Name = "Newtonsoft.Json")]
        NewtonsoftJson,
        [Display(Name = "Prism.DryIoc.Forms")]
        PrismDryIocForms,
        [Display(Name = "Prism.Plugin.Popups")]
        PrismPluginPopups,
        [Display(Name = "SQLite-net-pcl")]
        SQLitenetpcl,
        [Display(Name = "Xam.Plugins.Forms.ImageCircle")]
        XamPluginsFormsImageCircle,
        [Display(Name = "Xamarin.FFImageLoading.Svg.Forms")]
        XamarinFFImageLoadingSvgForms,
        [Display(Name = "Xamarin.Forms.PancakeView")]
        XamarinFormsPancakeView
    }
}
