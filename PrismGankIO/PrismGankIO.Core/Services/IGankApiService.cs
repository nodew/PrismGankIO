using PrismGankIO.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PrismGankIO.Core.Services
{
    public interface IGankApiService
    {
        Task<PagedResult<Post>> GetPostsAsync(Category category, string type, int page = 1, int size = 10);

        Task<PagedResult<Post>> GetHotPostsAsync(string hotType, Category category, int count = 10);

        Task<PagedResult<Post>> GetArticlesAsync(string type, int page = 1, int size = 10);

        Task<PagedResult<Post>> GetGirlsAsync(string type, int page = 1, int size = 10);

        Task<PagedResult<Post>> GetGanHuoAsync(string type, int page = 1, int size = 10);

        Task<HttpResult<SubType[]>> GetAvailableTypesAsync(Category category);

        Task<HttpResult<SubType[]>> GetAvailableTypesOfGanhuoAsync();

        Task<HttpResult<SubType[]>> GetAvailableTypesOfArticleAsync();

        Task<HttpResult<Banner[]>> GetBannersAsync();
    }
}
