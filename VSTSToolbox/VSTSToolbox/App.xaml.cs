using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;
using Syncfusion.Licensing;
using VSTSToolbox.API.RestService;
using VSTSToolbox.API.VSTS;
using VSTSToolbox.Models;
using VSTSToolbox.Services;

namespace VSTSToolbox
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static ViewModelLocator _locator;

        public static ViewModelLocator Locator => _locator ?? (_locator = new ViewModelLocator());

        public App()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Your License here");
            SimpleIoc.Default.Register<IRestClientFactory, RestClientFactory>();
            SimpleIoc.Default.Register<SettingsModel>();
            SimpleIoc.Default.Register<IVSTSServiceConfig, VSTSServiceConfig>();
            SimpleIoc.Default.Register<IVSTSService, VSTSService>();
        }
    }
}
