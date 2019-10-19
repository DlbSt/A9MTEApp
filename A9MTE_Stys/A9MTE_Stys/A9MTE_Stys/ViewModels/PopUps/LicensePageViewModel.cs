using A9MTE_Stys.Enums;
using A9MTE_Stys.Extensions;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Xamarin.Forms;

namespace A9MTE_Stys.ViewModels.PopUps
{
    public class LicensePageViewModel : BindableBase, IInitialize
    {
        #region Services
        private readonly INavigationService _navigationService;
        #endregion

        #region Commands
        public DelegateCommand ClosePopupCommand { get; set; }
        #endregion

        #region Properties
        private string htmlText = string.Empty;
        public string HtmlText
        {
            get { return htmlText; }
            set { SetProperty(ref htmlText, value); }
        }
        #endregion

        #region Fields

        private string newtonsoftJsonHeader = @"<h3>Newtonsoft.Json</h3>";
        private string newtonsoftJsonCopyright = @"<p>Copyright (c) 2007 James Newton-King</p>";

        private string prismFormsHeader = @"<h3>Prism.DryIoc.Forms</h3>";
        private string prismFormsCopyright = @"<p>Copyright (c) .NET Foundation</p>";

        private string prismPopUpsHeader = @"<h3>Prism.Plugin.Popups</h3>";
        private string prismPopUpsCopyright = @"<p>Copyright (c) 2016 Dan Siegel</p>";

        private string sqliteNetPclHeader = @"<h3>SQLite-net-pcl</h3>";
        private string sqliteNetPclCopyright = @"<p>Copyright (c) Krueger Systems, Inc.</p>";

        private string imageCircleHeader = @"<h3>Xam.Plugins.Forms.ImageCircle</h3>";
        private string imageCircleCopyright = @"<p>Copyright (c) 2016 James Montemagno / Refractored LLC</p>";

        private string ffImageSvgHeader = @"<h3>Xamarin.FFImageLoading.Svg.Forms</h3>";
        private string ffImageSvgCopyright = @"<p>Copyright (c) 2015 Daniel Luberda & Fabien Molinet</p>";

        private string pancakeViewHeader = @"<h3>Xamarin.Forms.PancakeView</h3>";
        private string pancakeViewCopyright = @"<p>Copyright (c) 2019 Steven Thewissen</p>";                                                                                         

        private string htmlHeader = @"<html><body>";
        private string htmlFooter = @"</body></html>";
        private string mitLicenseTitle = @"<p>The MIT License (MIT)</p>";
        private string mitLicenseFull = @"<p>Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the &#8221;Software&#8221;), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:</p>
                                          <p>The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.</p>
                                          <p>THE SOFTWARE IS PROVIDED &#8221;AS IS&#8221;, WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.</p>";

        #endregion

        public LicensePageViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            ClosePopupCommand = new DelegateCommand(ClosePopupAsync);
        }

        #region Navigation
        public void Initialize(INavigationParameters parameters)
        {
            if (LicenseEnum.NewtonsoftJson.GetAttribute<DisplayAttribute>().Name == parameters["libraryname"] as string) HtmlText = htmlHeader +
                                                                                                                                    newtonsoftJsonHeader +
                                                                                                                                    mitLicenseTitle +
                                                                                                                                    newtonsoftJsonCopyright +
                                                                                                                                    mitLicenseFull +
                                                                                                                                    htmlFooter;
            else if (LicenseEnum.PrismDryIocForms.GetAttribute<DisplayAttribute>().Name == parameters["libraryname"] as string) HtmlText = htmlHeader +
                                                                                                                                           prismFormsHeader +
                                                                                                                                           mitLicenseTitle +
                                                                                                                                           prismFormsCopyright +
                                                                                                                                           mitLicenseFull +
                                                                                                                                           htmlFooter;
            else if (LicenseEnum.PrismPluginPopups.GetAttribute<DisplayAttribute>().Name == parameters["libraryname"] as string) HtmlText = htmlHeader +
                                                                                                                                            prismPopUpsHeader +
                                                                                                                                            mitLicenseTitle +
                                                                                                                                            prismPopUpsCopyright +
                                                                                                                                            mitLicenseFull +
                                                                                                                                            htmlFooter;
            else if (LicenseEnum.SQLitenetpcl.GetAttribute<DisplayAttribute>().Name == parameters["libraryname"] as string) HtmlText = htmlHeader +
                                                                                                                                       sqliteNetPclHeader +
                                                                                                                                       mitLicenseTitle +
                                                                                                                                       sqliteNetPclCopyright +
                                                                                                                                       mitLicenseFull +
                                                                                                                                       htmlFooter;
            else if (LicenseEnum.XamarinFFImageLoadingSvgForms.GetAttribute<DisplayAttribute>().Name == parameters["libraryname"] as string) HtmlText = htmlHeader +
                                                                                                                                                        ffImageSvgHeader +
                                                                                                                                                        mitLicenseTitle +
                                                                                                                                                        ffImageSvgCopyright +
                                                                                                                                                        mitLicenseFull +
                                                                                                                                                        htmlFooter;
            else if (LicenseEnum.XamarinFormsPancakeView.GetAttribute<DisplayAttribute>().Name == parameters["libraryname"] as string) HtmlText = htmlHeader +
                                                                                                                                                  pancakeViewHeader +
                                                                                                                                                  mitLicenseTitle + 
                                                                                                                                                  pancakeViewCopyright +
                                                                                                                                                  mitLicenseFull +
                                                                                                                                                  htmlFooter;
            else if (LicenseEnum.XamPluginsFormsImageCircle.GetAttribute<DisplayAttribute>().Name == parameters["libraryname"] as string) HtmlText = htmlHeader +
                                                                                                                                                     imageCircleHeader +
                                                                                                                                                     mitLicenseTitle +
                                                                                                                                                     imageCircleCopyright +
                                                                                                                                                     mitLicenseFull +
                                                                                                                                                     htmlFooter;
        }

        private async void ClosePopupAsync()
        {
            await _navigationService.GoBackAsync();
        }
        #endregion
    }
}
