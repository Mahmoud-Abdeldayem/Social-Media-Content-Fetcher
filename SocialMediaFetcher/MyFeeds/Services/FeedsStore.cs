using MyFeeds.Models;
using System.Collections.Concurrent;

namespace MyFeeds.Services
{
    public static class FeedsStore
    {
        public static ConcurrentBag<Feed> Feeds { get; private set; } = new ();

		// I Can make a normal list and with a lock to prevent race condition
		public static void AddFeeds(IEnumerable<Feed> feeds)
		{
			foreach (var post in feeds)
			{
				Feeds.Add(post);
			}
		}

		public static List<Feed> GetAllFeeds() => Feeds.Where(f => f.Content != null).ToList();
	}
}
