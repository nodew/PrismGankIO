using Prism.Regions;
using PrismGankIO.Core.Models;
using PrismGankIO.Core.Services;

namespace PrismGankIO.Shared.ViewModels
{
    public class ArticlesPageViewModel : PostCollectionViewModel
    {
        public ArticlesPageViewModel(IGankApiService gankApiService, IRegionManager regionManager) 
            : base(gankApiService, regionManager)
        {
            _ = this.Initialize(Category.Article);
        }
    }
}
