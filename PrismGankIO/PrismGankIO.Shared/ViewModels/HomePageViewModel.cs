using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismGankIO.Core.Models;
using PrismGankIO.Core.Services;
using PrismGankIO.Shared.Constant;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PrismGankIO.Shared.ViewModels
{
    public class HomePageViewModel : BindableBase
    {
        private readonly IGankApiService gankApiService;

        private readonly IRegionManager regionManager;

        private ObservableCollection<Post> posts = new ObservableCollection<Post>();

        public HomePageViewModel(IGankApiService gankApiService, IRegionManager regionManager)
        {
            this.gankApiService = gankApiService;
            this.regionManager = regionManager;

            HandlePostItemClickedCmd = new DelegateCommand<Post>(
                HandlePostItemClicked,
                item => !string.IsNullOrEmpty(item.Url));

            _ = LoadDataAsync();
        }

        public ObservableCollection<Post> Posts 
        { 
            get { return posts; }
            set { SetProperty(ref posts, value); }
        }

        public DelegateCommand<Post> HandlePostItemClickedCmd { get; }

        private async Task LoadDataAsync()
        {
            HttpResult<List<Post>> httpResult = await gankApiService.GetHotPostsAsync(HotType.Views, Category.GanHuo);
            httpResult.Data.ForEach(item =>
            {
                posts.Add(item);
            });
        }

        private void HandlePostItemClicked(Post item)
        {
            NavigationParameters parameters = new NavigationParameters
            {
                { "PostUrl", item.Url }
            };
            regionManager.RequestNavigate(RegionNames.ContentRegion, Pages.PostDetailPage, parameters);
        }
    }
}
