using PrismGankIO.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrismGankIO.Core.Services
{
    public interface IGankApiService
    {
        Task<PagedResult<Post>> GetPostsAsync(Category category, string type, int page = 1, int size = 10, CancellationToken cancellationToken = default);

        Task<PagedResult<Post>> GetHotPostsAsync(string hotType, Category category, int count = 10, CancellationToken cancellationToken = default);

        Task<PagedResult<Post>> GetArticlesAsync(string type, int page = 1, int size = 10, CancellationToken cancellationToken = default);

        Task<PagedResult<Post>> GetGirlsAsync(int page = 1, int size = 10, CancellationToken cancellationToken = default);

        Task<PagedResult<Post>> GetGanHuoAsync(string type, int page = 1, int size = 10, CancellationToken cancellationToken = default);

        Task<HttpResult<List<SubType>>> GetAvailableTypesAsync(Category category, CancellationToken cancellationToken = default);

        Task<HttpResult<List<SubType>>> GetAvailableTypesOfGanhuoAsync(CancellationToken cancellationToken = default);

        Task<HttpResult<List<SubType>>> GetAvailableTypesOfArticleAsync(CancellationToken cancellationToken = default);

        Task<HttpResult<List<Banner>>> GetBannersAsync(CancellationToken cancellationToken = default);
    }
}
