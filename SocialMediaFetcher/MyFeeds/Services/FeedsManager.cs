using MyFeeds.Models;
using System.Diagnostics;

namespace MyFeeds.Services
{
    public class FeedsManager
    {
        private readonly FacebookFetcher _facebookFetcher;
		private readonly RedditFetcher _redditFetcher;
        private List<Thread> fetchingThreads;
		private Stopwatch stopWatch;

        public FeedsManager(FacebookFetcher facebookFetcher , RedditFetcher redditFetcher) 
        {
            _facebookFetcher = facebookFetcher;
			_redditFetcher = redditFetcher;
			stopWatch = Stopwatch.StartNew();
        }

        public void FetchFromAll()
        {
			fetchingThreads = new List<Thread>()
			{
				//Facebook Thread
				new Thread(() =>
				{
					var posts = _facebookFetcher.FetchPosts();
					Console.WriteLine($"Starting Thread: {Thread.CurrentThread.ManagedThreadId}");
					FeedsStore.AddFeeds(posts);
				}) , 

				//Reddit Thread
				new Thread(() =>
                {
                    var posts = _redditFetcher.FetchFeeds();
                    Console.WriteLine($"Starting Thread: {Thread.CurrentThread.ManagedThreadId}");
                    FeedsStore.AddFeeds(posts);
                })
            };

			stopWatch.Start();
			foreach(var thread in fetchingThreads)
			{
				
				thread.Start();
            }

			// Wait for all threads to complete
			foreach (var thread in fetchingThreads)
			{
				thread.Join();
			}
			stopWatch.Stop();
            Console.WriteLine($"Execution Completed , and the total time is {stopWatch.ElapsedMilliseconds}ms ....");
		}

    }
}
