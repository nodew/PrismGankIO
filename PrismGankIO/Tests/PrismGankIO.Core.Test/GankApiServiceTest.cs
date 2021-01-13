using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using PrismGankIO.Core.Services;
using PrismGankIO.Core.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace PrismGankIO.Core.Test
{
    [TestClass]
    public class GankApiServiceTest
    {
        private readonly GankApiService gankApiService;
        
        public GankApiServiceTest()
        {
            HttpClient httpClient = new HttpClient();
            gankApiService = new GankApiService(httpClient);
        }

        [TestMethod]
        public async Task GetAvailableTypesOfArticleAsync_Test()
        {
            HttpResult<List<SubType>> httpResult = await gankApiService.GetAvailableTypesOfArticleAsync();
            Assert.IsNotNull(httpResult);
            Assert.IsNotNull(httpResult.Data);
            Assert.IsTrue(httpResult.Data.Count > 0);
        }

        [TestMethod]

        public async Task GetAvailableTypesOfGanhuoAsync_Test()
        {
            HttpResult<List<SubType>> httpResult = await gankApiService.GetAvailableTypesOfGanhuoAsync();
            Assert.IsNotNull(httpResult);
            Assert.IsNotNull(httpResult.Data);
            Assert.IsTrue(httpResult.Data.Count > 0);
        }

        [TestMethod]
        public async Task GetBannersAsync_Test()
        {
            HttpResult<List<Banner>> httpResult = await gankApiService.GetBannersAsync();
            Assert.IsNotNull(httpResult);
            Assert.IsNotNull(httpResult.Data);
            Assert.IsTrue(httpResult.Data.Count > 0);
        }

        [TestMethod]
        public async Task GetArticlesAsync_Test()
        {
            HttpResult<List<SubType>> subTypes = await gankApiService.GetAvailableTypesOfArticleAsync();
            PagedResult<Post> posts = await gankApiService.GetArticlesAsync(subTypes.Data[0].Type);
            Assert.IsNotNull(posts);
            Assert.IsNotNull(posts.Data);
            Assert.IsTrue(posts.Data.Count > 0);
        }

        [TestMethod]
        public async Task GetGanHuoAsync_Test()
        {
            HttpResult<List<SubType>> subTypes = await gankApiService.GetAvailableTypesOfGanhuoAsync();
            PagedResult<Post> posts = await gankApiService.GetGanHuoAsync(subTypes.Data[0].Type);
            Assert.IsNotNull(posts);
            Assert.IsNotNull(posts.Data);
            Assert.IsTrue(posts.Data.Count > 0);
        }

        [TestMethod]
        public async Task GetGirlAsync_Test()
        {
            PagedResult<Post> posts = await gankApiService.GetGirlsAsync();
            Assert.IsNotNull(posts);
            Assert.IsNotNull(posts.Data);
            Assert.IsTrue(posts.Data.Count > 0);
        }
    }
}
