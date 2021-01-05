using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;

namespace PrismGankIO.Shared.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private string _selectedNavItem;
        private readonly IRegionManager _regionManager;

        public DelegateCommand<string> HandleSelectedNavItemCmd { get; }
        public DelegateCommand AfterPageLoadedCmd { get; }

        public string SelectedNavItem
        {
            get { return _selectedNavItem; }
            set { SetProperty(ref _selectedNavItem, value); }
        }

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SelectedNavItem = "Home";
            HandleSelectedNavItemCmd = new DelegateCommand<string>(HandleSelectedNavItem);
            AfterPageLoadedCmd = new DelegateCommand(() =>
            {
                _regionManager.RequestNavigate("ContentRegion", "HomePage");
            });
        }

        private void HandleSelectedNavItem (string tag)
        {
            SelectedNavItem = tag;
            _regionManager.RequestNavigate("ContentRegion", $"{tag}Page");
        }
    }
}
