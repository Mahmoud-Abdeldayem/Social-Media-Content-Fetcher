namespace MyFeeds.Models
{
    public class RedditResponse
    {
        public Data Data { get; set; }
    }

    public class Data
    {
        public Child[] Children { get; set; }
    }

    public class Child
    {
        public PostData Data { get; set; }
    }

    public class PostData
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public double CreatedUtc { get; set; }
        public string Selftext { get; set; }
        public string AuthorFullname { get; set; }
    }
}


