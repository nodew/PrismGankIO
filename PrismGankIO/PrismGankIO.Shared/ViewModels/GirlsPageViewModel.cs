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
    public class GirlsPageViewModel : BindableBase
    {
        private readonly IGankApiService gankApiService;

        private ObservableCollection<Post> posts = new ObservableCollection<Post>();

        private int currentPage;

        private readonly int pageSize = 20;

        private bool moreAvailable;

        private bool isLoading;

        public GirlsPageViewModel(IGankApiService gankApiService)
        {
            this.gankApiService = gankApiService;
            this.currentPage = 1;
            this.moreAvailable = true;
            _ = LoadData();
        }

        public ObservableCollection<Post> Posts
        {
            get { return posts; }
            set { SetProperty(ref posts, value); }
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

        private async Task LoadNextPage()
        {
            currentPage += 1;
            await LoadData();
        }

        private async Task LoadData()
        {
            if (!MoreAvailable)
            {
                return;
            }

            IsLoading = true;

            PagedResult<Post> result = await gankApiService.GetGirlsAsync(currentPage, pageSize);

            IsLoading = false;

            if (result.PageCount <= currentPage)
            {
                MoreAvailable = false;
            }

            result.Data.ForEach(item =>
            {
                Posts.Add(item);
            });
        }
    }
}
