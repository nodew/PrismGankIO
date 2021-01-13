using Microsoft.Extensions.Logging;
using Prism.DryIoc;
using Prism.Ioc;
using PrismGankIO.Shared.Views;
using Windows.ApplicationModel;
using Windows.UI.Xaml;
using System.Net.Http;
using PrismGankIO.Core.Services;
using PrismGankIO.Shared.Constant;

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

            this.InitializeComponent();
        }

        protected override UIElement CreateShell()
        {
            return Container.Resolve<Shell>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation(typeof(HomePage), SideNavTags.HomePage);
            containerRegistry.RegisterForNavigation(typeof(SettingPage), SideNavTags.SettingPage);
            containerRegistry.RegisterForNavigation(typeof(ArticlesPage), SideNavTags.ArticlesPage);
            containerRegistry.RegisterForNavigation(typeof(GanHuoPage), SideNavTags.GanhuoPage);
            containerRegistry.RegisterForNavigation(typeof(GirlsPage), SideNavTags.GirlsPage);

            containerRegistry.RegisterInstance(typeof(HttpClient), new HttpClient());
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
