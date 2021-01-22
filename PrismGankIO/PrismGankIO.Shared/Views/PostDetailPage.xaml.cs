using Prism.Regions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// https://go.microsoft.com/fwlink/?LinkId=234238 上介绍了“空白页”项模板

namespace PrismGankIO.Shared.Views
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class PostDetailPage : Page, INavigationAware, IRegionMemberLifetime
    {
        public PostDetailPage()
        {
            this.InitializeComponent();
        }

        public bool KeepAlive => false;

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        void PostDetailWebView_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            ProgressControl.IsActive = true;
        }

        void PostDetailWebView_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            ProgressControl.IsActive = false;
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.TryGetValue("PostUrl", out string url))
            {
                if (url.StartsWith("http"))
                {
                    PostDetailWebView.Source = new Uri(url);
                }
            }
        }

        private void OpenInBrowser(object sender, RoutedEventArgs e)
        {
            _ = Windows.System.Launcher.LaunchUriAsync(PostDetailWebView.Source);
        }
    }
}
