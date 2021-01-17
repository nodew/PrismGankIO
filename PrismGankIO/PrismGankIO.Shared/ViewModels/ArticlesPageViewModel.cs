using Prism.Mvvm;
using PrismGankIO.Core.Models;
using PrismGankIO.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace PrismGankIO.Shared.ViewModels
{
    public class ArticlesPageViewModel : BindableBase
    {
        private readonly IGankApiService gankApiService;

        private string selectedSubType;

        private ObservableCollection<Post> posts = new ObservableCollection<Post>();

        private ObservableCollection<SubType> subTypes = new ObservableCollection<SubType>();

        private int currentPage;

        private readonly int pageSize = 20;

        private bool moreAvailable;

        private bool isLoading;

        public ArticlesPageViewModel(IGankApiService gankApiService)
        {
            this.gankApiService = gankApiService;
            currentPage = 1;
            moreAvailable = true;
            isLoading = false;
            _ = Initialize();
        }

        public string SelectedSubType
        {
            get { return selectedSubType; }
            set { SetProperty(ref selectedSubType, value); }
        }

        public ObservableCollection<Post> Posts
        {
            get { return posts; }
            set { SetProperty(ref posts, value); }
        }

        public ObservableCollection<SubType> SubTypes
        {
            get { return subTypes; }
            set { SetProperty(ref subTypes, value); }
        }

        public bool MoreAvailable
        {
            get { return moreAvailable; }
            set { SetProperty(ref moreAvailable, value); }
        }

        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
        }

        private async Task Initialize()
        {
            IsLoading = true;
            HttpResult<List<SubType>> subTypes = await gankApiService.GetAvailableTypesOfArticleAsync();
            subTypes.Data.ForEach((type) =>
            {
                SubTypes.Add(type);
            });

            SelectedSubType = SubTypes[0].Type;

            await LoadPosts();
        }

        private async Task LoadNextPagePosts()
        {
            currentPage += 1;
            await LoadPosts();
        }

        private async Task LoadPosts()
        {
            if (!MoreAvailable)
            {
                return;
            }

            try
            {
                PagedResult<Post> result = await gankApiService.GetArticlesAsync(SelectedSubType, currentPage, pageSize);
            
                if (result.PageCount <= currentPage)
                {
                    MoreAvailable = false;
                }
            
                result.Data.ForEach((item) =>
                {
                    Posts.Add(item);
                });

                IsLoading = false;
            } 
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
    }
}
