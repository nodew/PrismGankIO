using PrismGankIO.Core.Models;
using PrismGankIO.Core.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Threading;

namespace PrismGankIO.Core.Services
{
    public class GankApiService : IGankApiService
    {
        private readonly HttpClient httpClient;
        private readonly JsonSerializerOptions serializerOptions;
        private readonly string baseUri = "https://gank.io/api/v2";

        public GankApiService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
            this.serializerOptions = new JsonSerializerOptions { 
                IgnoreNullValues = true, 
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase 
            };
            this.serializerOptions.Converters.Add(new DateTimeConverter());
        }

        public async Task<HttpResult<List<SubType>>> GetAvailableTypesAsync(Category category, CancellationToken cancellationToken = default)
        {
            string requestUri = $"{baseUri}/categories/{category}";
            return await GetDataAsync<HttpResult<List<SubType>>>(requestUri, cancellationToken);
        }

        public async Task<HttpResult<List<SubType>>> GetAvailableTypesOfArticleAsync(CancellationToken cancellationToken = default)
        {
            return await GetAvailableTypesAsync(Category.Article, cancellationToken);
        }

        public async Task<HttpResult<List<SubType>>> GetAvailableTypesOfGanhuoAsync(CancellationToken cancellationToken = default)
        {
            return await GetAvailableTypesAsync(Category.GanHuo, cancellationToken);
        }

        public async Task<HttpResult<List<Banner>>> GetBannersAsync(CancellationToken cancellationToken = default)
        {
            string requestUri = $"{baseUri}/banners";
            return await GetDataAsync<HttpResult<List<Banner>>>(requestUri, cancellationToken);
        }

        public async Task<PagedResult<Post>> GetArticlesAsync(string type, int page = 1, int size = 10, CancellationToken cancellationToken = default)
        {
            return await GetPostsAsync(Category.Article, type, page, size, cancellationToken);
        }

        public async Task<PagedResult<Post>> GetGanHuoAsync(string type, int page = 1, int size = 10, CancellationToken cancellationToken = default)
        {
            return await GetPostsAsync(Category.GanHuo, type, page, size, cancellationToken);
        }

        public async Task<PagedResult<Post>> GetGirlsAsync(int page = 1, int size = 10, CancellationToken cancellationToken = default)
        {
            return await GetPostsAsync(Category.Girl, "Girl", page, size, cancellationToken);
        }

        public async Task<PagedResult<Post>> GetHotPostsAsync(string hotType, Category category, int count = 10, CancellationToken cancellationToken = default)
        {
            string requestUri = $"{baseUri}/hot/{hotType}/category/{category}/count/{count}";
            return await GetDataAsync<PagedResult<Post>>(requestUri, cancellationToken);
        }

        public async Task<PagedResult<Post>> GetPostsAsync(Category category, string type, int page = 1, int size = 10, CancellationToken cancellationToken = default)
        {
            string requestUri = $"{baseUri}/data/category/{category}/type/{type}/page/{page}/count/{size}";
            return await GetDataAsync<PagedResult<Post>>(requestUri, cancellationToken);
        }

        private async Task<T> GetDataAsync<T>(string uri, CancellationToken cancellationToken = default)
        {
            using (var httpResponse = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                httpResponse.EnsureSuccessStatusCode();

                if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                    try
                    {
                        return await JsonSerializer.DeserializeAsync<T>(contentStream, serializerOptions, cancellationToken);
                    }
                    catch (JsonException ex)
                    {
                        Console.WriteLine(ex);
                        Console.WriteLine("Invalid JSON.");
                    }
                }
                else
                {
                    Console.WriteLine("HTTP Response was invalid and cannot be deserialised.");
                }

                throw new Exception("Failed to load data");
            }
        }
    }
}
