using Prism.Mvvm;
using PrismGankIO.Core.Models;
using PrismGankIO.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Prism.Commands;

namespace PrismGankIO.Shared.ViewModels
{
    public class ArticlesPageViewModel : BindableBase
    {
        private readonly IGankApiService gankApiService;

        private TypedPosts selectedItem;

        private ObservableCollection<TypedPosts> typedPosts = new ObservableCollection<TypedPosts>();

        private ObservableCollection<SubType> subTypes = new ObservableCollection<SubType>();

        public ArticlesPageViewModel(IGankApiService gankApiService)
        {
            this.gankApiService = gankApiService;
            HandleSelectedTypeChangedCmd = new DelegateCommand<TypedPosts>(
                HandleSelectedChanged, 
                (TypedPosts _selectedItem) => SelectedItem.Type != _selectedItem.Type
            );

            _ = Initialize();
        }

        public DelegateCommand<TypedPosts> HandleSelectedTypeChangedCmd { get; }

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

        private async Task Initialize()
        {
            HttpResult<List<SubType>> subTypes = await gankApiService.GetAvailableTypesOfArticleAsync();
            subTypes.Data.ForEach((type) =>
            {
                SubTypes.Add(type);
                TypedPosts.Add(new TypedPosts(gankApiService, Category.Article, type));
            });

            SelectedItem = TypedPosts[0];

            await LoadDataAsync();
        }

        private void HandleSelectedChanged(TypedPosts selectedItem)
        {
            SelectedItem = selectedItem;
            if (SelectedItem.Posts.Count == 0 && !SelectedItem.IsLoading)
            {
                _ = LoadDataAsync();
            }
        }

        private async Task LoadNextPageAsync()
        {
            await SelectedItem.LoadNextPageAsync();
        }

        private async Task LoadDataAsync()
        {
            await SelectedItem.LoadDataAsync();
        }
    }
}
