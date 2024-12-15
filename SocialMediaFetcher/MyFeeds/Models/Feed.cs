namespace MyFeeds.Models
{
    public class Feed
    {

        public int ThreadId { get; set; }
        public string Content { get; set; }
        public string FeedDate { get; set; }  

        //public string UserName { get; set; }

        public string Platform { get; set; }
    }
}
