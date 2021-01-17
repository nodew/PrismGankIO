using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismGankIO.Shared.Constant;
using System.Collections.Generic;
using System.Linq;
using Windows.UI.Xaml.Controls;

namespace PrismGankIO.Shared.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private string selectedNavItem;
        private string currentUri;
        private bool isBackEnabled;
        private NavigationViewBackButtonVisible backButtonVisible;

        private readonly IRegionManager regionManager;
        private IRegionNavigationService navigationService;
        private List<string> topPages = new List<string> { 
            Pages.HomePage, 
            Pages.GirlsPage, 
            Pages.ArticlesPage, 
            Pages.GanhuoPage, 
            Pages.SettingPage 
        };

        public ShellViewModel(IRegionManager regionManager)
        {
            this.SelectedNavItem = Pages.HomePage;
            this.BackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
            this.regionManager = regionManager;

            HandleSelectedNavItemCmd = new DelegateCommand<string>(HandleSelectedNavItem);
            GoBackCmd = new DelegateCommand(GoBack, CanGoBack);

            ContentRegionLoadCmd = new DelegateCommand(() =>
            {
                var region = regionManager.Regions[RegionNames.ContentRegion];
                navigationService = region.NavigationService;
                navigationService.Navigated += HandleNavigated;
            });
        }

        public DelegateCommand<string> HandleSelectedNavItemCmd { get; }

        public DelegateCommand GoBackCmd { get; }

        public DelegateCommand ContentRegionLoadCmd { get; }

        public string SelectedNavItem
        {
            get { return selectedNavItem; }
            set { SetProperty(ref selectedNavItem, value); }
        }

        public bool IsBackEnabled
        {
            get { return isBackEnabled; }
            set { SetProperty(ref isBackEnabled, value); }
        }

        public NavigationViewBackButtonVisible BackButtonVisible
        {
            get { return backButtonVisible; }
            set { SetProperty(ref backButtonVisible, value); }
        }

        private void HandleSelectedNavItem (string tag)
        {
            if (SelectedNavItem != tag)
            {
                SelectedNavItem = tag;
                regionManager.RequestNavigate(RegionNames.ContentRegion, tag);
            }
            else if (!currentUri.Contains(tag))
            {
                regionManager.RequestNavigate(RegionNames.ContentRegion, tag);
            }
        }

        private void HandleNavigated(object sender, RegionNavigationEventArgs e)
        {
            currentUri = e.Uri.ToString();
            if (topPages.Contains(currentUri)) 
            {
                IsBackEnabled = false;
                BackButtonVisible = NavigationViewBackButtonVisible.Collapsed;
            }
            else
            {
                IsBackEnabled = true;
                BackButtonVisible = NavigationViewBackButtonVisible.Visible;
            }
        }

        private void GoBack()
        {
            if (navigationService.Journal.CanGoBack)
            {
                navigationService.Journal.GoBack();
            }
        }

        private bool CanGoBack()
        {
            return navigationService.Journal.CanGoBack;
        }
    }
}
