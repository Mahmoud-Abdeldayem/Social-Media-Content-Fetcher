using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ServiceLayer
{
	public class FacebookFetcherService : IPlatFormFetcher
	{
		private readonly HttpClient _httpClient;

        private const string FacebookURL = "https://graph.facebook.com/v21.0/me?fields=posts&access_token=EAAH4pQFwTZBABO7G7nhZBKpjB7hrZBm2eUF3CfN1G4ZAFNB58muDKAHbQXO5XzKGSDQmTSWkydpqTpq1MHva2uS10KCTXICVHlRPqCeIkqJzJLfYsZC94XyuZC9oX3RrvZCa0sfg9c9mpnHbgDIXl72M7JeNGFmkJuHosznZCtUVc8bfbv7gOWHd2FDV";

        public FacebookFetcherService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public List<Feed> FetchFeeds()
		{
			return new List<Feed>();
		}

		/// <summary>
		///     This method responsable for fethcing facebook posts and Deserialize the response and map it to feeds
		/// </summary>
		/// <returns>IEnumerable of feeds</returns>
		public IEnumerable<Feed> FetchPosts()
		{

			#region Make a request to get posts
			var response = _httpClient.GetAsync(FacebookURL).Result;

			var posts = response.Content.ReadAsStringAsync().Result;
			#endregion

			#region Deserialize the JSON response
			var deserializedPosts = JsonSerializer.Deserialize<FPosts>(posts, new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			});
			#endregion

			#region Map the deserialized posts into feeds
			var feeds = MapFBPostsToFeeds(deserializedPosts.Posts.Data);
			#endregion

			return feeds;
		}

		private IEnumerable<Feed> MapFBPostsToFeeds(IEnumerable<FacebookPost> posts)
		{
			var feeds = posts.Select(f => new Feed
			{
				FeedDate = f.CreatedDate,
				Platform = "Facebook",
				Content = f.Message
			});
			return feeds;
		}
	}
}
