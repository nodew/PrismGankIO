using Microsoft.Toolkit.Collections;
using PrismGankIO.Core.Models;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using PrismGankIO.Core.Services;

namespace PrismGankIO.Shared.ViewModels
{
    public class PostCollectionSource : IIncrementalSource<Post>
    {
        private readonly IGankApiService gankApiService;

        private readonly Category category;
        
        private readonly string type;

        public PostCollectionSource(IGankApiService gankApiService, Category category, string type)
        {
            this.gankApiService = gankApiService;
            this.category = category;
            this.type = type;
        }

        public async Task<IEnumerable<Post>> GetPagedItemsAsync(int pageIndex, int pageSize, CancellationToken cancellationToken = default)
        {
            PagedResult<Post> result = await gankApiService.GetPostsAsync(category, type, pageIndex + 1, pageSize, cancellationToken);
            return result.Data;  
        }
    }
}
