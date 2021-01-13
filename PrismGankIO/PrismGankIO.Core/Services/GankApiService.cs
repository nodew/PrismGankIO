using PrismGankIO.Core.Models;
using PrismGankIO.Core.Converters;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

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

        public async Task<HttpResult<List<SubType>>> GetAvailableTypesAsync(Category category)
        {
            string requestUri = $"{baseUri}/categories/{category}";
            return await GetDataAsync<HttpResult<List<SubType>>>(requestUri);
        }

        public async Task<HttpResult<List<SubType>>> GetAvailableTypesOfArticleAsync()
        {
            return await GetAvailableTypesAsync(Category.Article);
        }

        public async Task<HttpResult<List<SubType>>> GetAvailableTypesOfGanhuoAsync()
        {
            return await GetAvailableTypesAsync(Category.GanHuo);
        }

        public async Task<HttpResult<List<Banner>>> GetBannersAsync()
        {
            string requestUri = $"{baseUri}/banners";
            return await GetDataAsync<HttpResult<List<Banner>>>(requestUri);
        }

        public async Task<PagedResult<Post>> GetArticlesAsync(string type, int page = 1, int size = 10)
        {
            return await GetPostsAsync(Category.Article, type, page, size);
        }

        public async Task<PagedResult<Post>> GetGanHuoAsync(string type, int page = 1, int size = 10)
        {
            return await GetPostsAsync(Category.GanHuo, type, page, size);
        }

        public async Task<PagedResult<Post>> GetGirlsAsync(int page = 1, int size = 10)
        {
            return await GetPostsAsync(Category.Girl, "Girl", page, size);
        }

        public async Task<PagedResult<Post>> GetHotPostsAsync(string hotType, Category category, int count = 10)
        {
            string requestUri = $"{baseUri}/hot/{hotType}/category/{category}/count/{count}";
            return await GetDataAsync<PagedResult<Post>>(requestUri);
        }

        public async Task<PagedResult<Post>> GetPostsAsync(Category category, string type, int page = 1, int size = 10)
        {
            string requestUri = $"{baseUri}/data/category/{category}/type/{type}/page/{page}/count/{size}";
            return await GetDataAsync<PagedResult<Post>>(requestUri);
        }

        private async Task<T> GetDataAsync<T>(string uri)
        {
            using (var httpResponse = await httpClient.GetAsync(uri, HttpCompletionOption.ResponseHeadersRead))
            {
                httpResponse.EnsureSuccessStatusCode();

                if (httpResponse.Content is object && httpResponse.Content.Headers.ContentType.MediaType == "application/json")
                {
                    var contentStream = await httpResponse.Content.ReadAsStreamAsync();

                    try
                    {
                        return await JsonSerializer.DeserializeAsync<T>(contentStream, serializerOptions);
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
