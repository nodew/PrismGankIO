﻿using Microsoft.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using PrismGankIO.Shared.Views;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using System.Net.Http;
using PrismGankIO.Core.Services;
using PrismGankIO.Shared.Constant;
using System;

namespace PrismGankIO
{
    /// <summary>
    /// Provides application-specific behavior to supplement the default Application class.
    /// </summary>
    sealed partial class App : PrismApplication
    {
        /// <summary>
        /// Initializes the singleton application object.  This is the first line of authored code
        /// executed, and as such is the logical equivalent of main() or WinMain().
        /// </summary>
        public App()
        {
            ConfigureFilters(global::Uno.Extensions.LogExtensionPoint.AmbientLoggerFactory);
#if WINDOWS_UWP
            Windows.ApplicationModel.Resources.Core.ResourceContext.SetGlobalQualifierValue("custom", "uwp");
#endif
            this.InitializeComponent();
        }

        protected override UIElement CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(typeof(HomePage), Pages.HomePage);
            containerRegistry.RegisterForNavigation(typeof(SettingPage), Pages.SettingPage);
            containerRegistry.RegisterForNavigation(typeof(ArticlesPage), Pages.ArticlesPage);
            containerRegistry.RegisterForNavigation(typeof(GanHuoPage), Pages.GanhuoPage);
            containerRegistry.RegisterForNavigation(typeof(GirlsPage), Pages.GirlsPage);
            containerRegistry.RegisterForNavigation(typeof(ImageDetailPage), Pages.ImageDetailPage);
            containerRegistry.RegisterForNavigation(typeof(PostDetailPage), Pages.PostDetailPage);

            HttpClient httpClient = new HttpClient();
            httpClient.Timeout = TimeSpan.FromSeconds(60);

            containerRegistry.RegisterInstance(typeof(HttpClient), httpClient);
            containerRegistry.RegisterSingleton(typeof(IGankApiService), typeof(GankApiService));
        }

        /// <summary>
        /// Invoked when application execution is being suspended.  Application state is saved
        /// without knowing whether the application will be terminated or resumed with the contents
        /// of memory still intact.
        /// </summary>
        /// <param name="sender">The source of the suspend request.</param>
        /// <param name="e">Details about the suspend request.</param>
        protected override void OnSuspending(SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }

        /// <summary>
        /// Configures global logging
        /// </summary>
        /// <param name="factory"></param>
        static void ConfigureFilters(ILoggerFactory factory)
        {
            factory
                .WithFilter(new FilterLoggerSettings
                    {
                        { "Uno", LogLevel.Warning },
                        { "Windows", LogLevel.Warning }
					}
                )
                .AddConsole(LogLevel.Information);
        }
    }
}
