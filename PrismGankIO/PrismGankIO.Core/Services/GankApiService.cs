using PrismGankIO.Core.Models;
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
        }

        public async Task<PagedResult<Post>> GetArticlesAsync(string type, int page = 1, int size = 10)
        {
            return await GetPostsAsync(Category.Article, type, page, size);
        }

        public async Task<HttpResult<SubType[]>> GetAvailableTypesAsync(Category category)
        {
            string requestUri = $"{baseUri}/{category}";
            return await GetDataAsync<HttpResult<SubType[]>>(requestUri);
        }

        public async Task<HttpResult<SubType[]>> GetAvailableTypesOfArticleAsync()
        {
            return await GetAvailableTypesAsync(Category.Article);
        }

        public async Task<HttpResult<SubType[]>> GetAvailableTypesOfGanhuoAsync()
        {
            return await GetAvailableTypesAsync(Category.GanHuo);
        }

        public async Task<HttpResult<Banner[]>> GetBannersAsync()
        {
            string requestUri = $"{baseUri}/banner";
            return await GetDataAsync<HttpResult<Banner[]>>(requestUri);
        }

        public async Task<PagedResult<Post>> GetGanHuoAsync(string type, int page = 1, int size = 10)
        {
            return await GetPostsAsync(Category.GanHuo, type, page, size);
        }

        public async Task<PagedResult<Post>> GetGirlsAsync(string type, int page = 1, int size = 10)
        {
            return await GetPostsAsync(Category.Girl, type, page, size);
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
                    catch (JsonException) // Invalid JSON
                    {
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
