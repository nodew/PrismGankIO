using Microsoft.Toolkit.Collections;
using Microsoft.Toolkit.Uwp;
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

        private readonly IncrementalLoadingCollection<PostCollectionSource, Post> posts;
        
        public TypedPosts(IGankApiService gankApiService, Category category, SubType type)
        {
            this.category = category;
            this.type = type.Type;
            this.title = type.Title;

            PostCollectionSource source = new PostCollectionSource(gankApiService, category, this.type);

            this.posts = new IncrementalLoadingCollection<PostCollectionSource, Post>(source, 20);
        }

        public IncrementalLoadingCollection<PostCollectionSource, Post> Posts
        {
            get { return posts; }
        }

        public string Title
        {
            get { return title; }
        }

        public string Type
        {
            get { return type; }
        }

        public Category Category
        {
            get { return category; }
        }
    }
}
