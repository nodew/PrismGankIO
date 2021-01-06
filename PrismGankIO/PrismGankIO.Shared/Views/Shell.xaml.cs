using Prism.Regions;
using PrismGankIO.Shared.Constant;
using PrismGankIO.Shared.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace PrismGankIO.Shared.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Shell : Page
    {
        private readonly ShellViewModel viewModel;

        public Shell(IRegionManager regionManger)
        {
            InitializeComponent();

            viewModel = (ShellViewModel)this.DataContext;
            
            SetSelectedItemByTag(viewModel.SelectedNavItem);

            viewModel.PropertyChanged += (object sender, PropertyChangedEventArgs e) => {
                if (e.PropertyName.Equals(nameof(viewModel.SelectedNavItem)))
                {
                    SetSelectedItemByTag(viewModel.SelectedNavItem);
                }
            };

            regionManger.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(HomePage));
        }

        private void SetSelectedItemByTag(string tag)
        {
            if (tag == SideNavTags.SettingPage)
            {
                MainNavigationView.SelectedItem = MainNavigationView.SettingsItem;
            } 
            else
            {
                MainNavigationView.SelectedItem = MainNavigationView.MenuItems.Where(item => ((Framework​Element)item).Tag.ToString().Equals(tag)).FirstOrDefault();
            }
        }

        private void HandleSelectionChanged(object sender, NavigationViewSelectionChangedEventArgs e)
        {
            string tag;
            if (e.IsSettingsSelected)
            {
                tag = SideNavTags.SettingPage;
            }
            else
            {
                tag = ((FrameworkElement)e.SelectedItem).Tag.ToString();
            }

            viewModel.HandleSelectedNavItemCmd.Execute(tag);
        }
    }
}
