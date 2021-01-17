using Prism.Regions;
using PrismGankIO.Core.Models;
using PrismGankIO.Shared.Constant;
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

namespace PrismGankIO.Shared.Views
{
    public sealed partial class GirlsPage : Page
    {
        private readonly IRegionManager regionManager;

        public GirlsPage(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            this.InitializeComponent();
        }

        private void ImageItemClick(object sender, ItemClickEventArgs e)
        {
            if (e.ClickedItem != null)
            {
                Post item = e.ClickedItem as Post;
                NavigationParameters parameters = new NavigationParameters();
                parameters.Add("ImageUrl", item.Url);
                regionManager.RequestNavigate(RegionNames.ContentRegion, Pages.ImageDetailPage, parameters);
            }
        }
    }
}
