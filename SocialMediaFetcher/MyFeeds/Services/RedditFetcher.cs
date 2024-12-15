using MyFeeds.Models;
using MyFeeds.Services.Interfaces;
using System.Text.Json;

namespace MyFeeds.Services
{
    public class RedditFetcher : IPlatFormFetcher
    {
        private readonly HttpClient _httpClient;

        private const string RedditURL = "https://oauth.reddit.com/r/learnprogramming/new/.json?limit=10";
        public RedditFetcher(HttpClient httpClient) 
        {
            _httpClient = httpClient;
        }
        public List<Feed> FetchFeeds()
        {
            #region Make a request to get Reddit posts
            var requestMessage = new HttpRequestMessage(HttpMethod.Get , RedditURL);
            requestMessage.Headers.Add("User-Agent", "PostmanRuntime/7.43.0");
            requestMessage.Headers.Add("Authorization", "eyJhbGciOiJSUzI1NiIsImtpZCI6IlNIQTI1NjpzS3dsMnlsV0VtMjVmcXhwTU40cWY4MXE2OWFFdWFyMnpLMUdhVGxjdWNZIiwidHlwIjoiSldUIn0.eyJzdWIiOiJ1c2VyIiwiZXhwIjoxNzM0MzI2ODc0LjAyNDg4MSwiaWF0IjoxNzM0MjQwNDc0LjAyNDg4MSwianRpIjoiS3Q4YVM3c3pEdksxQVpsWXdtTWVhQ2JPOU9PdEp3IiwiY2lkIjoiYkszRHB5amhqRHlKQm1vcUdWYU8wUSIsImxpZCI6InQyXzEyNzlja2l4eG4iLCJhaWQiOiJ0Ml8xMjc5Y2tpeHhuIiwibGNhIjoxNzE4MDMwMjM1Nzk2LCJzY3AiOiJlSnlLVnRKU2lnVUVBQURfX3dOekFTYyIsImZsbyI6OX0.fcsjjFHLGQ4fB0g37nFNgTzLt-v3ni9cJ8Iog2ogZ2-hk92tZL6VWANKNIFjEkdMxpWq0IM8FaBeFctqz0U6l6rltAlEtWnlSIUXr-KQ9eRISKEoLlJzP3hBm_xKUlYo72Uqas194VvB6d4XbVQsUUY1c2WprEoQ8gbZB44xNc1cxp-pJWsUsNlfujd7BQFW5Ts7q3keaOy-ufWtxPJU54G4GsHKS6LdM-olJK9fceNSQWYsFaJ-_aZ1SX2nNgKGcl6wURMDfEVwH3MJUif_Yurr83KQVUjOvfna7w0Xyldz3R2zh_1UfY6MpB6su5KMsjtm7QMJc3wu9NFx8sDXHg");

            var response = _httpClient.SendAsync(requestMessage).Result;
            Console.WriteLine(response);
            var posts = response.Content.ReadAsStringAsync().Result;
            #endregion

            #region Deserialize the JSON response
            var deserializedPosts = JsonSerializer.Deserialize<RedditResponse>(posts, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            });
            #endregion

            #region Map the deserialized posts into feeds
            var feeds = MapRedditPostsToFeeds(deserializedPosts);
            #endregion

            return feeds.ToList();
        }

        private IEnumerable<Feed> MapRedditPostsToFeeds(RedditResponse posts)
        {
            var feeds = posts.Data.Children.Select((child, index) => new Feed
            {
                Content = child.Data.Selftext,
                FeedDate = DateTimeOffset.FromUnixTimeSeconds((long)child.Data.CreatedUtc)
                             .ToString("yyyy-MM-dd HH:mm:ss"),  
                Platform = "Reddit" 
            });

            return feeds;
        }

    }
}



