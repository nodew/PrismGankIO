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

        private string selectedSubType;

        private ObservableCollection<TypedPosts> typedPosts = new ObservableCollection<TypedPosts>();

        private ObservableCollection<SubType> subTypes = new ObservableCollection<SubType>();

        public ArticlesPageViewModel(IGankApiService gankApiService)
        {
            this.gankApiService = gankApiService;
            HandleSelectedTypeChangedCmd = new DelegateCommand<string>(HandleSelectedChanged, (string type) => SelectedSubType != type);

            _ = Initialize();
        }

        public DelegateCommand<string> HandleSelectedTypeChangedCmd { get; }

        public string SelectedSubType
        {
            get { return selectedSubType; }
            set { SetProperty(ref selectedSubType, value); }
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

        public TypedPosts ActiveItem
        {
            get { return TypedPosts.Where(item => item.Type == SelectedSubType).FirstOrDefault(); }
        }

        private async Task Initialize()
        {
            HttpResult<List<SubType>> subTypes = await gankApiService.GetAvailableTypesOfArticleAsync();
            subTypes.Data.ForEach((type) =>
            {
                SubTypes.Add(type);
                TypedPosts.Add(new TypedPosts(gankApiService, Category.Article, type));
            });

            SelectedSubType = SubTypes[0].Type;

            await LoadDataAsync();
        }

        private void HandleSelectedChanged(string type)
        {
            SelectedSubType = type;
            if (ActiveItem.Posts.Count == 0)
            {
                _ = LoadDataAsync();
            }
        }

        private async Task LoadNextPageAsync()
        {
            await ActiveItem.LoadNextPageAsync();
        }

        private async Task LoadDataAsync()
        {
            await ActiveItem.LoadDataAsync();
        }
    }
}
