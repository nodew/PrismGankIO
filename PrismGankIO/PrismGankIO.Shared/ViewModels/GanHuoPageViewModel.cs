using Prism.Regions;
using PrismGankIO.Core.Models;
using PrismGankIO.Core.Services;

namespace PrismGankIO.Shared.ViewModels
{
    public class GanHuoPageViewModel : PostCollectionViewModel
    {
        public GanHuoPageViewModel(IGankApiService gankApiService, IRegionManager regionManager) 
            : base(gankApiService, regionManager)
        {
            _ = this.Initialize(Category.GanHuo);
        }
    }
}
