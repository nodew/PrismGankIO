using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismGankIO.Shared.Constant;

namespace PrismGankIO.Shared.ViewModels
{
    public class ShellViewModel : BindableBase
    {
        private string _selectedNavItem;
        private readonly IRegionManager _regionManager;

        public ShellViewModel(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            SelectedNavItem = "Home";
            HandleSelectedNavItemCmd = new DelegateCommand<string>(HandleSelectedNavItem);
        }

        public DelegateCommand<string> HandleSelectedNavItemCmd { get; }

        public string SelectedNavItem
        {
            get { return _selectedNavItem; }
            set { SetProperty(ref _selectedNavItem, value); }
        }

        private void HandleSelectedNavItem (string tag)
        {
            SelectedNavItem = tag;
            _regionManager.RequestNavigate(RegionNames.ContentRegion, tag);
        }
    }
}
