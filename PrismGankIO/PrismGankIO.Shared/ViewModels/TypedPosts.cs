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
    public class TypedPosts : BindableBase
    {
        private readonly Category category;
        private readonly string type;
        private readonly string title;
        private readonly int pageSize;
        private bool moreAvailable;
        private bool isLoading;
        private int currentPage;

        private ObservableCollection<Post> posts;
        
        private readonly IGankApiService gankApiService;

        public TypedPosts(IGankApiService gankApiService, Category category, SubType type)
        {
            this.gankApiService = gankApiService;
            this.category = category;
            this.type = type.Type;
            this.title = type.Title;
            this.Posts = new ObservableCollection<Post>();
            this.moreAvailable = true;
            this.isLoading = false;
            this.currentPage = 1;
            this.pageSize = 20;
        }

        public ObservableCollection<Post> Posts
        {
            get { return posts; }
            set { SetProperty(ref posts, value); }
        }

        public string Title
        {
            get { return title; }
        }

        public string Type
        {
            get { return type; }
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

        public async Task LoadNextPageAsync()
        {
            currentPage += 1;
            await LoadDataAsync();
        }

        public async Task LoadDataAsync()
        {
            if (!MoreAvailable || IsLoading)
            {
                return;
            }

            try
            {
                PagedResult<Post> result = await gankApiService.GetPostsAsync(category, type, currentPage, pageSize);

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
