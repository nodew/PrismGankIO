using Microsoft.Toolkit.Uwp;
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
    public class GirlsPageViewModel : BindableBase
    {
        private IncrementalLoadingCollection<PostCollectionSource, Post> posts;
        private readonly IRegionManager regionManager;

        public GirlsPageViewModel(IGankApiService gankApiService, IRegionManager regionManager)
        {
            this.regionManager = regionManager;
            PostCollectionSource source = new PostCollectionSource(gankApiService, Category.Girl, "Girl");
            this.posts = new IncrementalLoadingCollection<PostCollectionSource, Post>(source, 20);

            HandleImageItemClickCmd = new DelegateCommand<Post>(HandleImageItemClick, (item) => !String.IsNullOrEmpty(item.Url));
        }

        public IncrementalLoadingCollection<PostCollectionSource, Post> Posts
        {
            get { return posts; }
        }

        public DelegateCommand<Post> HandleImageItemClickCmd { get; }

        private void HandleImageItemClick(Post item)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("ImageUrl", item.Url);
            regionManager.RequestNavigate(RegionNames.ContentRegion, Pages.ImageDetailPage, parameters);
        }
    }
}
