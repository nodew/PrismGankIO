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
    public class PostCollectionViewModel : BindableBase
    {
        private readonly IGankApiService gankApiService;

        private readonly IRegionManager regionManager;

        private TypedPosts selectedItem;

        private ObservableCollection<TypedPosts> typedPosts = new ObservableCollection<TypedPosts>();

        private ObservableCollection<SubType> subTypes = new ObservableCollection<SubType>();

        public PostCollectionViewModel(IGankApiService gankApiService, IRegionManager regionManager)
        {
            this.gankApiService = gankApiService;
            this.regionManager = regionManager;
            HandleSelectedTypeChangedCmd = new DelegateCommand<TypedPosts>(
                HandleSelectedChanged,
                (TypedPosts _selectedItem) => SelectedItem.Type != _selectedItem.Type
            );
            HandlePostItemClickedCmd = new DelegateCommand<Post>(HandlePostItemClicked);
        }

        public DelegateCommand<TypedPosts> HandleSelectedTypeChangedCmd { get; }

        public DelegateCommand<Post> HandlePostItemClickedCmd { get; }

        public TypedPosts SelectedItem
        {
            get { return selectedItem; }
            set { SetProperty(ref selectedItem, value); }
        }

        public ObservableCollection<TypedPosts> TypedPosts
        {
            get { return typedPosts; }
            set { SetProperty(ref typedPosts, value); }
        }

        public ObservableCollection<SubType> SubTypes
        {
            get { return subTypes; }
            set { SetProperty(ref subTypes, value); }
        }

        public async Task Initialize(Category category)
        {
            HttpResult<List<SubType>> subTypes = await gankApiService.GetAvailableTypesAsync(category);
            subTypes.Data.ForEach((type) =>
            {
                SubTypes.Add(type);
                TypedPosts.Add(new TypedPosts(gankApiService, category, type));
            });

            SelectedItem = TypedPosts[0];
        }

        private void HandleSelectedChanged(TypedPosts selectedItem)
        {
            SelectedItem = selectedItem;
        }

        private void HandlePostItemClicked(Post item)
        {
            NavigationParameters parameters = new NavigationParameters();
            parameters.Add("PostUrl", item.Url);
            regionManager.RequestNavigate(RegionNames.ContentRegion, Pages.PostDetailPage, parameters);
        }
    }
}
